ko.bindingHandlers["aceEditor"] = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        ace.config.set("basePath", "../Scripts/ace");

        var editor = ace.edit(element);
        editor.setTheme("ace/theme/chrome");
        editor.setFontSize(20);
        editor.setShowPrintMargin(false);
        editor.setShowFoldWidgets(false);
        editor.setShowInvisibles(false);
        editor.setDisplayIndentGuides(false);
        editor.session.setMode("ace/mode/" + allBindingsAccessor.get("aceEditor-language").toLowerCase());
        editor.session.setOption("useWorker", false);
        editor.session.on("change", function () {
            var prop = valueAccessor();
            if (ko.isObservable(prop)) {
                element.dotvvmUpdating = true;
                prop(editor.session.getValue());
                element.dotvvmUpdating = false;
            }
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.unwrap(valueAccessor());

        if (!element.dotvvmUpdating) {
            var editor = ace.edit(element);
            editor.session.setValue(value);
        }
    }
};