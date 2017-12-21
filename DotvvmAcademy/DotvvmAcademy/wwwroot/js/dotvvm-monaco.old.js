ko.bindingHandlers["dotvvm-monaco"] = {
    init: function (element, valueAccessor) {
        require.config({ baseUrl: "/", paths: { 'vs': 'monaco-editor/min/vs' } });
        require(['vs/editor/editor.main'], function () {
            let binding = valueAccessor();
            var editor = monaco.editor.create(element, {
                value: ko.unwrap(binding.code),
                language: ko.unwrap(binding.language),
                theme: ko.unwrap(binding.theme),
                codeLens: false,
                scrollBeyondLastLine: false,
                contextmenu: false,
                fontSize: 16,
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
        binding.code.subscribe((n) => this.updateValue(n, element));
        binding.markers.subscribe((n)=>this.updateMarkers(n, element));
        //if (element.dotvvmIgnoreUpdate || element.monaco === undefined) {
        //    element.dotvvmIgnoreUpdate = false;
        //    return;
        //}
        //element.dotvvmIgnoreUpdate = true;
        //element.monaco.setValue(code);
        //let model = element.monaco.getModel();
        //monaco.editor.setModelMarkers(markers);
    },
    updateValue: function (newValue, element) {
        if (element.dotvvmIgnoreUpdate) {
            element.dotvvmIgnoreUpdate = false;
            return;
        }
        element.monaco.setValue(newValue);
    },
    updateMarkers: function (newMarkers, element) {
        let model = element.monaco.getModel();
        monaco.editor.setModelMarkers(markers);
    }
}