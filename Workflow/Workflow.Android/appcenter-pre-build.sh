#!/usr/bin/env bash
ACTIVITY=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/MainActivity.cs
# ACTIVITY=MainActivity.cs
MANIFEST=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/Properties/AndroidManifest.xml
LABEL_VAR=${LABEL}
PACKAGE=${PACKAGE_NAME}
# MANIFEST=Properties/AndroidManifest.xml
ICON=Resources/drawable

sed -i '' 's/Label = "[-а-яА-Яa-zA-Z0-9|[[:space:]]]*"s/Label = "'$LABEL_VAR'"/'  $ACTIVITY
echo ${LABEL_VAR}
cat ${ACTIVITY}
exit 1;
# sed -i "s/package=\"[a-z | .]*\"/package=\"${PACKAGE}\"/" ${MANIFEST}
# sed -i "s/package=\"[-a-z]*\"/package=\"ru.meme.meme\"" ${MANIFEST}