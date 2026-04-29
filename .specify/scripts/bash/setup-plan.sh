#!/usr/bin/env bash

set -e

# Parse command line arguments
JSON_MODE=false
FEATURE_ID=""
ARGS=()

i=1
while [ $i -le $# ]; do
    arg="${!i}"
    case "$arg" in
        --json)
            JSON_MODE=true
            ;;
        --feature)
            if [ $((i + 1)) -gt $# ]; then
                echo 'Error: --feature requires a value' >&2
                exit 1
            fi
            i=$((i + 1))
            next_arg="${!i}"
            if [[ "$next_arg" == --* ]]; then
                echo 'Error: --feature requires a value' >&2
                exit 1
            fi
            FEATURE_ID="$next_arg"
            ;;
        --help|-h)
            echo "Usage: $0 [--json] [--feature <feature-id>]"
            echo "  --json               Output results in JSON format"
            echo "  --feature <id>       Specify feature by number or name (e.g. 0001 or 0001-feature-name)"
            echo "  --help               Show this help message"
            exit 0
            ;;
        *)
            ARGS+=("$arg")
            ;;
    esac
    i=$((i + 1))
done

# Get script directory and load common functions
SCRIPT_DIR="$(CDPATH="" cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/common.sh"

# When --feature is provided, resolve the spec file path by numeric prefix or full name
# and export SPECIFY_FEATURE_DIRECTORY so get_feature_paths() picks it up.
if [[ -n "$FEATURE_ID" ]]; then
    _repo_root=$(get_repo_root)
    _specs_dir="$_repo_root/.specify/specs"
    _resolved_spec=""

    # Try glob: match FEATURE_ID as a prefix (e.g. "0001" matches "0001-remove-weather.md")
    for _f in "$_specs_dir/${FEATURE_ID}"*.md; do
        if [[ -f "$_f" ]]; then
            _resolved_spec="$_f"
            break
        fi
    done

    if [[ -n "$_resolved_spec" ]]; then
        export SPECIFY_FEATURE_DIRECTORY="$_resolved_spec"
    else
        echo "ERROR: No spec file found for feature '$FEATURE_ID' in $_specs_dir" >&2
        exit 1
    fi
    unset _repo_root _specs_dir _resolved_spec _f
fi

# Get all paths and variables from common functions
_paths_output=$(get_feature_paths) || { echo "ERROR: Failed to resolve feature paths" >&2; exit 1; }
eval "$_paths_output"
unset _paths_output

# If feature.json pins an existing feature directory, branch naming is not required.
# Also skip branch validation when the feature was explicitly specified via --feature.
if [[ -z "$FEATURE_ID" ]] && ! feature_json_matches_feature_dir "$REPO_ROOT" "$FEATURE_DIR"; then
    check_feature_branch "$CURRENT_BRANCH" "$HAS_GIT" || exit 1
fi

# Ensure the plans directory exists
mkdir -p "$(dirname "$IMPL_PLAN")"

# Copy plan template if it exists
TEMPLATE=$(resolve_template "plan-template" "$REPO_ROOT") || true
if [[ -n "$TEMPLATE" ]] && [[ -f "$TEMPLATE" ]]; then
    cp "$TEMPLATE" "$IMPL_PLAN"
    echo "Copied plan template to $IMPL_PLAN"
else
    echo "Warning: Plan template not found"
    # Create a basic plan file if template doesn't exist
    touch "$IMPL_PLAN"
fi

# Output results
if $JSON_MODE; then
    if has_jq; then
        jq -cn \
            --arg feature_spec "$FEATURE_SPEC" \
            --arg impl_plan "$IMPL_PLAN" \
            --arg specs_dir "$FEATURE_DIR" \
            --arg branch "$CURRENT_BRANCH" \
            --arg has_git "$HAS_GIT" \
            '{FEATURE_SPEC:$feature_spec,IMPL_PLAN:$impl_plan,SPECS_DIR:$specs_dir,BRANCH:$branch,HAS_GIT:$has_git}'
    else
        printf '{"FEATURE_SPEC":"%s","IMPL_PLAN":"%s","SPECS_DIR":"%s","BRANCH":"%s","HAS_GIT":"%s"}\n' \
            "$(json_escape "$FEATURE_SPEC")" "$(json_escape "$IMPL_PLAN")" "$(json_escape "$FEATURE_DIR")" "$(json_escape "$CURRENT_BRANCH")" "$(json_escape "$HAS_GIT")"
    fi
else
    echo "FEATURE_SPEC: $FEATURE_SPEC"
    echo "IMPL_PLAN: $IMPL_PLAN" 
    echo "SPECS_DIR: $FEATURE_DIR"
    echo "BRANCH: $CURRENT_BRANCH"
    echo "HAS_GIT: $HAS_GIT"
fi

