  /*
      Filename: Site.js
      Purpose: Filter subytpes based on the type selected when a new product is created.
      Author: Team Delta
      Methods: $(document).ready -> An ajaz function calls the GetSubTypesForDropdown to retrieve all subtypes. Then based on the value
                                    of the selected type, appends the selected types subtypes to the dom inside a select statement.
                                    The select statement is cleared each time to prevent repeating, and the select is only shown
                                    after a type has been selected.
  */


$(document).ready(function () {
  var subtypes = []

  $("#UserId").on("change", function (e) {
    $.ajax({
      url: `/Users/Activate/${$(this).val()}`,
      method: "POST",
      dataType: "json",
      contentType: 'application/json; charset=utf-8'
    }).done(() => {
      location.reload();
    });
  });

  $.ajax({
    url: "/Products/GetSubTypesForDropdown",
    method: "GET",
    headers: {
      "Content-Type": "application/json"
    }
  })
  .success(function(subTypes){
      subtypes.push(subTypes);
  });

  $(".ProductSubTypeId, .subDropdownLabel").hide();
  
  $(".ProductTypeId").on("change", function (e) {
    $(".ProductSubTypeId, .subDropdownLabel").show();
    $(".ProductSubTypeId").html(`<select></select>`);
    for(var key in subtypes[0]) {
        if(subtypes[0][key].productTypeId == $(this).val()){
            $(".ProductSubTypeId").append(`<option value="${subtypes[0][key].productSubTypeId}">${subtypes[0][key].name}</option>`);
        }
    }
    $(".ProductSubTypeId").prepend(`<option value="0" selected>Select a subtype!</option>`);
  })

  $(".completeOrder").on("click", function (e) {
    window.location.href = `/Orders/Process/${$(".paymentTypes").val()}`
  });

});