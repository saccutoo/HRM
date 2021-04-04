function Search(mvcSearchUrl, searchKey, partialView)
{
    $.ajax(
    {
        url: mvcSearchUrl,
        type: "POST",
        data: {
            searchKey: searchKey
        },
        success: function (response) {
            if (partialView != undefined && partialView != '')
            {
                $("#" + partialView).html(response);
            }
        }
    });
}