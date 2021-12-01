var pages = document.getElementsByClassName("page");
for(var i=0;i < pages.length;i++){
  pages[i].style.height = pages[i].getElementsByTagName("input")[0].value + "px";
  alert(pages[i].getElementsByTagName("input")[0].value);
}