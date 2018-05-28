//Servent:onAfterGet
//範例: 改變title欄位顏色

Item objItem ;
string strTitle;
string strColor;

for(int i=0; i < this.getItemCount(); i++){
    objItem = this.getItemByIndex(i);

    strColor="#B3FF99";

    objItem.setProperty("css", ".title { background-color: " + strColor +" }");
}
return this;
