var select = document.getElementById('selectNumber');
var categories = availableCategories;
for (var key in categories) {
    var opt = categories[key]
    var el = document.createElement("option");
    el.textContent = opt;
    el.value = opt;
    select.appendChild(el);
}
function change()
{
    var val = document.getElementById('selectNumber').value;
    var categories = availableCategories;
    for( var key in categories)
    {
        
        if(categories[key]===val)
        {
            document.getElementById('cat_box').value = key;
        }
    }
}
function change1()
{
    var val = document.getElementById('selectNumber').value;
    var categories = availableCategories;
    for (var key in categories) {

        if (categories[key] === val) {
            document.getElementById('cat_box1').value = categories[key];
        }
    }
}

function newChange()
{
    var x = document.getElementById('chk').checked;
    if(x==false)
    {
        document.getElementById('yes').value = 'N';
    }
    else if(x==true)
    {
        document.getElementById('yes').value = 'Y';
    }
}
function newChange1() {
    var x = document.getElementById('chk1').checked;
    if (x == false) {
        document.getElementById('txt2').value = 'N';
    }
    else if (x == true) {
        document.getElementById('txt2').value = 'Y';
    }
}
