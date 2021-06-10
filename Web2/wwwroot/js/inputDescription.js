function inputDescription(node) {

    //if (node.options.description !== undefined) {
        document.querySelector(".pop-up-label").hidden = false;
        document.querySelector(".pop-up-block").textContent = node.options.description;
    //}

    try {
        if (node.options.done == "true") {
            document.querySelector('.doneCheck').checked = true;
        }
        else {
            document.querySelector('.doneCheck').checked = false;
        }
    } catch (e) { }
}
