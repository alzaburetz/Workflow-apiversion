#!/usr/bin/env bash
ACTIVITY=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/MainActivity.cs
# ACTIVITY=MainActivity.cs
MANIFEST=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/Properties/AndroidManifest.xml
LABEL_VAR=${LABEL}
PACKAGE=${PACKAGE_NAME}
# MANIFEST=Properties/AndroidManifest.xml
ICON=Resources/drawable

sed -i '' 's/Label = "[a-zA-Z0-9|[[:space:]]]*"s/Label = "'$LABEL_VAR'"/'  $ACTIVITY
# sed -i "s/package=\"[a-z | .]*\"/package=\"${PACKAGE}\"/" ${MANIFEST}
# sed -i "s/package=\"[-a-z]*\"/package=\"ru.meme.meme\"" ${MANIFEST}