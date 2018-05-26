declare var ko;
declare var require;
declare var monaco;

ko.bindingHandlers["dotvvm-monaco"] = {
    init(element: HTMLElement, value: () => any) {
        let binding = value();
        require.config({ baseUrl: "/", paths: { "vs": "libs/monaco" } });
        require(["vs/editor/editor.main"], () => {
            let editor = monaco.editor.create(element, {
                value: binding.code(),
                language: binding.language,
                codeLens: false,
                scrollBeyondLastLine: false,
                contextmenu: false,
                fontSize: 16,
                theme: "vs-dark",
                minimap: {
                    enabled: false
                }
            });
        })
    }
}