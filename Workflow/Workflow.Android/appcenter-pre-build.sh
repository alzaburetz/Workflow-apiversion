#!/usr/bin/env bash
ACTIVITY=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Droid/MainActivity.cs
# ACTIVITY=MainActivity.cs
MANIFEST=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Droid/Properties/AndroidManifest.xml
# MANIFEST=Properties/AndroidManifest.xml
sed -i "s/Label = \"[-a-zA-Z0-9_ ]*\"/Label = \"${LABEL}\"/" ${ACTIVITY} |
sed -i "s/package=\"[a-z | .]*\"/package=\"${PACKAGE_NAME}\"/" ${MANIFEST}
# sed -i "s/package=\"[-a-z]*\"/package=\"ru.meme.meme\"" ${MANIFEST}