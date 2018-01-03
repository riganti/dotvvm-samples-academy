declare let require: any;

namespace DotvvmAcademy {

    interface ServerMonacoMarker {
        Code: KnockoutObservable<string>
        EndColumn: KnockoutObservable<number>
        EndLineNumber: KnockoutObservable<number>
        Message: KnockoutObservable<string>
        Source: KnockoutObservable<string>
        Severity: KnockoutObservable<number>
        StartColumn: KnockoutObservable<number>
        StartLineNumber: KnockoutObservable<number>
    }

    interface MonacoBinding {
        code: KnockoutObservable<string>
        language: string
        theme: string
        markers: KnockoutObservableArray<KnockoutObservable<ServerMonacoMarker>>
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
            if (this.binding.markers) {
                this.binding.markers.subscribe(this.onMarkersChanged.bind(this));
            }

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
            this.onMarkersChanged(this.binding.markers.peek());
            this.editor.onDidChangeModelContent(this.onModelChanged.bind(this));
        }

        onCodeChanged(newCode) {
            if (this.ignoreCodeChanged) {
                this.ignoreCodeChanged = false;
                return;
            }
            this.ignoreModelChanged = true;
            this.editor.setValue(this.binding.code())
        }

        onMarkersChanged(serverMarkers: KnockoutObservable<ServerMonacoMarker>[]) {
            let markers = serverMarkers.map(mapMonacoMarker);
            monaco.editor.setModelMarkers(this.editor.getModel(), null, markers);
        }

        onModelChanged(args) {
            if (this.ignoreModelChanged) {
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

    function mapMonacoMarker(serverMarkerObservable: KnockoutObservable<ServerMonacoMarker>): monaco.editor.IMarkerData {
        let serverMarker = serverMarkerObservable.peek();
        return {
            code: serverMarker.Code.peek(),
            endColumn: serverMarker.EndColumn.peek(),
            endLineNumber: serverMarker.EndLineNumber.peek(),
            message: serverMarker.Message.peek(),
            severity: serverMarker.Severity.peek(),
            source: serverMarker.Source.peek(),
            startColumn: serverMarker.StartColumn.peek(),
            startLineNumber: serverMarker.StartLineNumber.peek()
        };
    }

    MonacoEditor.installBindingHandler();
}