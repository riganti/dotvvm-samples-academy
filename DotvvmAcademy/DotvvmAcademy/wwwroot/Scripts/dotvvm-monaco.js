ko.bindingHandlers["dotvvm-monaco"] = {
    init: function (element, valueAccessor) {
        require.config({ paths: { 'vs': 'monaco-editor/min/vs' } });
        require(['vs/editor/editor.main'], function () {
            let binding = valueAccessor();
            var editor = monaco.editor.create(element, {
                value: ko.unwrap(binding.code),
                language: ko.unwrap(binding.language),
                theme: ko.unwrap(binding.theme),
                codeLens: false,
                minimap: {
                    enabled: false
                }
            });
            editor.onDidChangeModelContent(args => {
                if (element.dotvvmIgnoreUpdate) {
                    element.dotvvmIgnoreUpdate = false;
                    return;
                }
                let binding = valueAccessor();
                element.dotvvmIgnoreUpdate = true;
                binding.code(editor.getValue());
            });
            element.monaco = editor;
        });
    },
    update: function (element, valueAccessor) {
        let binding = valueAccessor();
        let code = ko.unwrap(binding.code);
        if (element.dotvvmIgnoreUpdate || element.monaco === undefined) {
            element.dotvvmIgnoreUpdate = false;
            return;
        }
        element.dotvvmIgnoreUpdate = true;
        element.monaco.setValue(code);
    }
}