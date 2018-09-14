declare let require;

require.config({ paths: { "vs": "/libs/monaco-editor/min/vs" } });
require(["vs/editor/editor.main"], () => { });

ko.bindingHandlers["dotvvm-monaco"] = {
    init(element: HTMLElement, value: () => DotvvmAcademy.IEditorBinding) {
        require(["vs/editor/editor.main"], () => {
            new DotvvmAcademy.Editor(element, value());
        });
    }
}

namespace DotvvmAcademy {
    export let isMonacoLoaded = false;

    export interface IServerMarker {
        Message: KnockoutObservable<string>,
        Severity: KnockoutObservable<string>,
        StartLineNumber: KnockoutObservable<number>,
        StartColumn: KnockoutObservable<number>,
        EndLineNumber: KnockoutObservable<number>,
        EndColumn: KnockoutObservable<number>
    }

    export interface IEditorBinding {
        code: KnockoutObservable<string>,
        language: string,
        markers: KnockoutObservableArray<KnockoutObservable<IServerMarker>>
    }

    export class Editor {
        element: HTMLElement
        binding: IEditorBinding
        editor: monaco.editor.IStandaloneCodeEditor
        ignoreChange: boolean

        constructor(element: HTMLElement, binding: IEditorBinding) {
            this.element = element;
            this.binding = binding;
            this.tryApplySessionStorage();
            this.initializeEditor();
            this.binding.code.subscribe(this.onCodeChange.bind(this));
            // this is complete and utter bollocks
            ko.computed(() => ko.toJSON(this.binding))
                .subscribe(_ => this.onMarkersChanged());
        }

        initializeEditor() {
            this.editor = monaco.editor.create(this.element, {
                value: this.binding.code(),
                language: this.getLanguage(this.binding.language),
                codeLens: false,
                scrollBeyondLastLine: false,
                contextmenu: false,
                theme: "vs-dark",
                minimap: {
                    enabled: false
                },
                renderWhitespace: "all",
                fontSize: 17
            });
            this.editor.onDidChangeModelContent(this.onEditorChange.bind(this));
        }

        onMarkersChanged() {
            let model = this.editor.getModel();
            let markers: monaco.editor.IMarkerData[] = [];
            for (let observable of this.binding.markers()) {
                let serverMarker = observable();
                if (serverMarker.StartLineNumber() == -1
                    || serverMarker.StartColumn() == -1
                    || serverMarker.EndLineNumber() == -1
                    || serverMarker.EndColumn() == -1) {
                    continue;
                }
                markers.push({
                    message: serverMarker.Message(),
                    severity: this.getSeverity(serverMarker.Severity()),
                    startLineNumber: serverMarker.StartLineNumber(),
                    startColumn: serverMarker.StartColumn(),
                    endLineNumber: serverMarker.EndLineNumber(),
                    endColumn: serverMarker.EndColumn()
                });
            }
            monaco.editor.setModelMarkers(this.editor.getModel(), null, markers);
        }

        getLanguage(language: string): string {
            switch (language) {
                case "csharp":
                    return "csharp";
                case "dothtml":
                    // TODO: Dothtml syntax highlighting
                    return "html";
                default:
                    return "plain";
            }
        }

        getSeverity(severity: string): monaco.MarkerSeverity {
            switch (severity) {
                case "Error":
                    return monaco.MarkerSeverity.Error;
                case "Warning":
                    return monaco.MarkerSeverity.Warning;
                case "Info":
                    return monaco.MarkerSeverity.Info;
                case "Hint":
                    return monaco.MarkerSeverity.Hint;
                default:
                    return monaco.MarkerSeverity.Error;
            }
        }

        onEditorChange(e: monaco.editor.IModelContentChangedEvent) {
            if (this.ignoreChange) {
                this.ignoreChange = false;
                return;
            }

            this.ignoreChange = true
            this.binding.code(this.editor.getValue());
        }

        onCodeChange(value: string) {
            this.saveToSessionStorage();
            if (this.ignoreChange) {
                this.ignoreChange = false;
                return;
            }

            this.ignoreChange = true;
            this.editor.setValue(value);
        }

        tryApplySessionStorage(): boolean {
            var code = sessionStorage.getItem(`CodeTask_${location.pathname}`);
            if (code != null) {
                this.binding.code(code);
                return true;
            }
            return false;
        }

        saveToSessionStorage() {
            sessionStorage.setItem(`CodeTask_${location.pathname}`, this.binding.code());
        }
    }
}