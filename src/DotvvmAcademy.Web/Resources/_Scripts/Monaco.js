ko.bindingHandlers["dotvvm-monaco"] = {
    init: function (element, value) {
        var binding = value();
        require.config({ baseUrl: "/", paths: { "vs": "libs/monaco" } });
        require(["vs/editor/editor.main"], function () {
            var editor = monaco.editor.create(element, {
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
        });
    }
};
//# sourceMappingURL=Monaco.js.map