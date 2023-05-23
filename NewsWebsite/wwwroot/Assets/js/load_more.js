//  LOAD MORE FUNCTION

$(document).ready(function(){
    $(".user__comments").slice(0, 3).show();
    $(".load__more").on("click", function(e){
      e.preventDefault();
      $(".user__comments:hidden").slice(0, 3).slideDown();
      if($(".user__comments:hidden").length == 0) {
        $(".load__more").text("No Content").addClass("noContent");
      }
    });
    
  })