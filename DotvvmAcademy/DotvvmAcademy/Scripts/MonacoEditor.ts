declare let require: any;

namespace DotvvmAcademy {

    interface MonacoBinding {
        code: KnockoutObservable<string>
        language: string
        theme: string
        markers: KnockoutObservableArray<any>
    }

    class MonacoEditor {
        element: HTMLElement
        binding: MonacoBinding
        editor: monaco.editor.IStandaloneCodeEditor
        ignoreModelChanged: boolean
        ignoreCodeChanged: boolean

        constructor(element: HTMLElement, binding: MonacoBinding) {
            this.element = element;
            this.binding = binding;
        }

        initialize() {
            this.binding.code.subscribe(this.onCodeChanged.bind(this));
            this.binding.markers.subscribe(this.onMarkersChanged.bind(this));

            require.config({ baseUrl: "/", paths: { 'vs': 'monaco-editor/min/vs' } });
            require(['vs/editor/editor.main'], this.onMonacoLoaded.bind(this))
        }

        onMonacoLoaded() {
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
            this.editor.onDidChangeModelContent(this.onModelChanged.bind(this));
        }

        onCodeChanged(newCode) {
            if(this.ignoreCodeChanged) {
                this.ignoreCodeChanged = false;
                return;
            }
            this.ignoreModelChanged = true;
            this.editor.setValue(this.binding.code())
        }

        onMarkersChanged(newMarkers) {
            monaco.editor.setModelMarkers(this.editor.getModel(), "test", newMarkers);
        }

        onModelChanged(args) {
            if(this.ignoreModelChanged) {
                this.ignoreModelChanged = false;
                return;
            }
            this.ignoreCodeChanged = true;
            this.binding.code(this.editor.getValue());
        }

        static installBindingHandler() {
            ko.bindingHandlers["dotvvm-monaco"] = {
                init(element: HTMLElement, valueAccessor: () => any) {
                    let control = new MonacoEditor(element, valueAccessor());
                    control.initialize();
                }
            }
        }
    }
    MonacoEditor.installBindingHandler();
}