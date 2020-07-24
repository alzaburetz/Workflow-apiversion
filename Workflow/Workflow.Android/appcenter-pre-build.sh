#!/usr/bin/env bash
ACTIVITY=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/MainActivity.cs
# ACTIVITY=MainActivity.cs
MANIFEST=${APPCENTER_SOURCE_DIRECTORY}/Workflow/Workflow.Android/Properties/AndroidManifest.xml
LABEL_VAR=${LABEL}
PACKAGE=${PACKAGE_NAME}
# MANIFEST=Properties/AndroidManifest.xml
FILE=${APPCENTER_SOURCE_DIRECTORY}/Workflow/App.xaml
cat ${FILE}
exit 1
ICON=Resources/drawable

sed -i.bak "s/Label = \"[А-Яа-я|' ']*\"/Label = \"${LABEL_VAR}\"/"  $ACTIVITY
rm -f ${ACTIVITY}.bak

cat ${ACTIVITY}
echo
sed -i.bak "s/package=\"[a-z | . | _ ]*\"/package=\"${PACKAGE}\"/" ${MANIFEST}
rm -f ${MANIFEST}.bak
sed -i.bak "s/label=\"[-a-zA-ZА-Яа-я | ' ']*\"/label=\"${LABEL_VAR}\"/" ${MANIFEST}
rm -f ${MANIFEST}.bak
cat ${MANIFEST}