declare let require;
require.config({ baseUrl: "/", paths: { "vs": "libs/monaco" } });
require(["vs/editor/editor.main"], () => DotvvmAcademy.isMonacoLoaded = true);
ko.bindingHandlers["dotvvm-monaco"] = {
    init(element: HTMLElement, value: () => DotvvmAcademy.IEditorBinding) {
        if (!DotvvmAcademy.isMonacoLoaded) {
            let interval = setInterval(() => {
                if (DotvvmAcademy.isMonacoLoaded) {
                    clearInterval(interval);
                    new DotvvmAcademy.Editor(element, value());
                }
            }, 100);
        }
        else {
            new DotvvmAcademy.Editor(element, value());
        }
    }
}

namespace DotvvmAcademy {
    export let isMonacoLoaded = false;

    export interface ICodeTaskDiagnostic {
        Start: KnockoutObservable<number>,
        End: KnockoutObservable<number>,
        Message: KnockoutObservable<string>
    }

    export interface IEditorBinding {
        code: KnockoutObservable<string>,
        language: string,
        diagnostics: KnockoutObservableArray<KnockoutObservable<ICodeTaskDiagnostic>>
    }

    export class Editor {
        element: HTMLElement
        binding: IEditorBinding
        editor: monaco.editor.IStandaloneCodeEditor

        constructor(element: HTMLElement, binding: IEditorBinding) {
            this.element = element;
            this.binding = binding;
            this.initializeEditor();
            this.binding.diagnostics.subscribe(this.onDiagnosticsChange.bind(this));
        }

        initializeEditor() {
            this.editor = monaco.editor.create(this.element, {
                value: this.binding.code(),
                language: this.binding.language,
                codeLens: false,
                scrollBeyondLastLine: false,
                contextmenu: false,
                fontSize: 16,
                theme: "vs-dark",
                minimap: {
                    enabled: false
                }
            });
            this.editor.onDidChangeModelContent(this.onTextChange.bind(this));
        }

        onDiagnosticsChange(diagnostics: KnockoutObservable<ICodeTaskDiagnostic>[]) {
            let model = this.editor.getModel();
            let markers: monaco.editor.IMarkerData[] = [];
            for (let observable of diagnostics) {
                let diagnostic = observable();
                if (diagnostic.Start() < 0 || diagnostic.End() < 0) {
                    continue;
                }
                let startPosition = model.getPositionAt(diagnostic.Start());
                let endPosition = model.getPositionAt(diagnostic.End());
                markers.push({
                    message: diagnostic.Message(),
                    severity: monaco.MarkerSeverity.Error,
                    startLineNumber: startPosition.lineNumber,
                    startColumn: startPosition.column,
                    endLineNumber: endPosition.lineNumber,
                    endColumn: endPosition.column
                });
            }
            monaco.editor.setModelMarkers(this.editor.getModel(), null, markers);
        }

        onTextChange(e: monaco.editor.IModelContentChangedEvent) {
            this.binding.code(this.editor.getValue());
        }
    }
}

