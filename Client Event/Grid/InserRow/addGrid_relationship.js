/*
example youtube url : https://youtu.be/Cnw9_74_NB4
*/
var inn = new Innovator();

console.log("-----------start------------");

//此表單物件
var tmpThisItem = typeof(parent.document.thisItem) == "object" ? parent.document.thisItem : parent.thisItem;


//尋找關聯頁籤上的第一個物件資料
var thisRel = tmpThisItem.getItemsByXPath("//Item[@id='" + relationshipID + "']").getItemByIndex(0);

//修改物件資料
try{
thisRel.setProperty("cn_1", "1");
thisRel.setProperty("cn_2", "1000");
}catch(e){
    console.log(e);
}


//填寫至畫面Grid
// 欄位_D 欄位_R : D代表關聯頁籤上的欄位，R代表關聯Related_id物件的欄位
grid.items_Experimental.set(relationshipID, "value", "cn_1_D", "1");
grid.items_Experimental.set(relationshipID, "value", "cn_2_D", "1000");