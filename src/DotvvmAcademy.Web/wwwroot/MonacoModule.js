import * as monaco from "monaco-editor";
export default (context) => new MonacoModule(context);
class MonacoModule {
    constructor(context) {
        this.context = context;
        ko.bindingHandlers["dotvvm-monaco"] = {
            init(element, value) {
                highlightSnippets();
                new Editor(element, value());
            }
        };
    }
}
let isMonacoLoaded = false;
function highlightSnippets() {
    let snippets = document.querySelectorAll("code[class^=language-]");
    for (var i = 0; i < snippets.length; i++) {
        let snippet = snippets[i];
        if (snippet.classList.length != 1) {
            throw new Error("Snippet must have a single class.");
        }
        let language = snippet.classList[0].substr(9, snippet.classList[0].length - 8);
        if (language == "dothtml") {
            language = "html";
        }
        snippet.setAttribute("data-lang", language);
        monaco.editor.colorizeElement(snippet, { tabSize: 4 });
    }
}
class Editor {
    constructor(element, binding) {
        this.element = element;
        this.binding = binding;
        this.tryApplySessionStorage();
        this.initializeEditor();
        this.binding.code.subscribe(this.onCodeChange.bind(this));
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
        window.addEventListener("resize", () => this.resize());
        this.resize();
    }
    resize() {
        this.editor.layout({
            width: this.element.parentElement.clientWidth,
            height: this.element.parentElement.clientHeight
        });
    }
    onMarkersChanged() {
        let markers = [];
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
    getLanguage(language) {
        switch (language) {
            case "csharp":
                return "csharp";
            case "dothtml":
                return "html";
            default:
                return "plain";
        }
    }
    getSeverity(severity) {
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
    onEditorChange(e) {
        if (this.ignoreChange) {
            this.ignoreChange = false;
            return;
        }
        this.ignoreChange = true;
        this.binding.code(this.editor.getValue());
    }
    onCodeChange(value) {
        this.saveToSessionStorage();
        if (this.ignoreChange) {
            this.ignoreChange = false;
            return;
        }
        this.ignoreChange = true;
        this.editor.setValue(value);
    }
    tryApplySessionStorage() {
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
