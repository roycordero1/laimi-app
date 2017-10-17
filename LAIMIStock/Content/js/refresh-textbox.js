function refreshTextBoxFors(obj) {
    var dropdown = document.getElementById("supplies-dropdown");
    var supplyId = dropdown.options[dropdown.selectedIndex].value;
    var supplyId2 = dropdown.value;    

    var supplyId3 = $("#supplies-dropdown").val();
    $("#cantidad").val(supplyId3);

    /*
    function refreshTextBoxFors() { 
        $("#bagelname").val($("#myDDL").val()); 
        index = $("#bagelname").val(); 
    } 
    <td>Bagelnaam</td><td> @Html.TextBoxFor(x => x.LstBagels.ElementAt(index).Name,new { Name ="txtEditBagelName" , id="test"})</td>
    */
}