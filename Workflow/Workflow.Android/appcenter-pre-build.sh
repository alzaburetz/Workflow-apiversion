#!/usr/bin/env bash

APP_DISPLAY_NAME="test"
ANDROID_MAINACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Droid/MainActivity.cs

echo "##[section][Pre-Build Action] Variable from env TEST = ${TEST}"

sed -i '' "s/Label = \"[-a-zA-Z0-9_ ]*\"/Label = \"${APP_DISPLAY_NAME}\"/" ${ANDROID_MAINACTIVITY_FILE}

    echo "##[section][Pre-Build Action] - MainActivity.cs File content:"
    cat ${ANDROID_MAINACTIVITY_FILE}