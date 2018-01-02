var DotvvmAcademy;
(function (DotvvmAcademy) {
    var MonacoEditor = (function () {
        function MonacoEditor(element, binding) {
            this.element = element;
            this.binding = binding;
        }
        MonacoEditor.prototype.initialize = function () {
            this.binding.code.subscribe(this.onCodeChanged.bind(this));
            if (this.binding.markers) {
                this.binding.markers.subscribe(this.onMarkersChanged.bind(this));
            }
            require.config({ baseUrl: "/", paths: { 'vs': 'monaco-editor/min/vs' } });
            require(['vs/editor/editor.main'], this.onMonacoLoaded.bind(this));
        };
        MonacoEditor.prototype.onMonacoLoaded = function () {
            this.editor = monaco.editor.create(this.element, {
                value: ko.unwrap(this.binding.code),
                language: this.binding.language,
                theme: this.binding.theme,
                codeLens: false,
                scrollBeyondLastLine: false,
                contextmenu: false,
                fontSize: 16,
                minimap: {
                    enabled: false
                }
            });
            this.onMarkersChanged(this.binding.markers());
            this.editor.onDidChangeModelContent(this.onModelChanged.bind(this));
        };
        MonacoEditor.prototype.onCodeChanged = function (newCode) {
            if (this.ignoreCodeChanged) {
                this.ignoreCodeChanged = false;
                return;
            }
            this.ignoreModelChanged = true;
            this.editor.setValue(this.binding.code());
        };
        MonacoEditor.prototype.onMarkersChanged = function (newMarkers) {
            monaco.editor.setModelMarkers(this.editor.getModel(), "test", newMarkers);
        };
        MonacoEditor.prototype.onModelChanged = function (args) {
            if (this.ignoreModelChanged) {
                this.ignoreModelChanged = false;
                return;
            }
            this.ignoreCodeChanged = true;
            this.binding.code(this.editor.getValue());
        };
        MonacoEditor.installBindingHandler = function () {
            ko.bindingHandlers["dotvvm-monaco"] = {
                init: function (element, valueAccessor) {
                    var control = new MonacoEditor(element, valueAccessor());
                    control.initialize();
                }
            };
        };
        return MonacoEditor;
    }());
    MonacoEditor.installBindingHandler();
})(DotvvmAcademy || (DotvvmAcademy = {}));
