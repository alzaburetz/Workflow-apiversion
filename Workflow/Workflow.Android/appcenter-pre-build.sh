#!/usr/bin/env bash
PACKAGE_NAME=${PACKAGE}
ANDROID_MAINACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/MainActivity.cs
APP_DISPLAY_NAME="AAAAAAAAAAA"
echo "##[section][Pre-Build Action] Variable from env TEST = ${TEST}"
sed -i '' "s/Label = \"[-a-zA-Z0-9_ ]*\"/Label = \"${APP_DISPLAY_NAME}\"/" ${ANDROID_MAINACTIVITY_FILE}
    echo "##[section][Pre-Build Action] - MainActivity.cs File content:"
    cat ${ANDROID_MAINACTIVITY_FILE}