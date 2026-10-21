# Unity project rules

Unity 6000.3.15f1 · Universal Render Pipeline · New Input System.

## Do not touch
- `Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/`
- `.meta` files: never edit by hand, never delete one without its asset.

## Rules
- Input only through the New Input System (`InputAction` or the generated actions class).
- Shaders and materials must target URP.
- Keep gameplay values in serialized fields or ScriptableObjects, not hard-coded.

## Verify
- The Unity console must show no compile errors after a change.
- Edit Mode tests, with the editor closed for this project:
  `& "C:\Program Files\Unity\Hub\Editor\6000.3.15f1\Editor\Unity.exe" -batchmode -projectPath . -runTests -testPlatform EditMode -testResults TestResults.xml`
