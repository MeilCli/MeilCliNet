$(function () {
    $('#scroll').infinitescroll({
        loading: {
            finishedMsg: "",
            msgText: "<em>読み込み中...</em>",
            selector: null,
            speed: 'fast',
        },
        navSelector: ".navigation",
        nextSelector: ".navigation a",
        itemSelector: ".scroll_content"
    });
});