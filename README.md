# Aras-PLM-Exercise
>本人於2016年進入Aras的程式設計行業，
>這些日子記錄程式碼的撰寫，
>因此做為往後回憶與複習時當參考，
>程式碼只公開當參考使用，本人不負責任何後續更新。

>若有程式上可精進的部分，歡迎建議與討論。

>>[]()

##Server Event
>Itemtype Server Event
>>[1.檢查Part的BOM列表中所有的Part，cn_group_id是否都與第一個Part相同](Aras-PLM-Exercise/Server Event/Itemtype Server Event/bcs_AutoBOMAddGroupId.txt) #relationship #update

>>[2.檢查DCO的變更項目是否重複](Aras-PLM-Exercise/Server Event/Itemtype Server Event/bcs_ChkReviewClassification.txt) #polyitem

>>[3.在onBeforeAdd時，自動插入第幾週期](Aras-PLM-Exercise/Server Event/Itemtype Server Event/bcs_getWeekOfYear.txt) #weektime

>Workflow Server Event
>>[1.利用頁籤上的簽審人員名單，自動插入會簽成員](Aras-PLM-Exercise/Server Event/Workflow Server Event/AutoAssignment.txt) #assignment

>>[2.workflow 零組件,廢止料件state2狀態推播](Aras-PLM-Exercise/Server Event/Workflow Server Event/bcs_NPR_Part_Repeal_UpState.txt) #relationship

>>[3.利用程式推動工作流程節點](Aras-PLM-Exercise/Server Event/Workflow Server Event/bcs_chkPartOnlyOneForECR_UpWF.txt) #EvaluateActivity

>>[4.判斷機種內主料及替代料未發行無法推關](Aras-PLM-Exercise/Server Event/Workflow Server Event/bcs_chkaffectItemreleased.txt) #major_rev 

>Lifecycle Server Event
>>[1.在生命週期的流程上，用程式啟動workflow](Aras-PLM-Exercise/Server Event/Lifecycle Server Event/bcs_create_workflow.txt) #instantiateWorkflow


##Client Event
>Action
>>[1.取得領號規則，並且依據Itemtype欄位的iq_temprary_preamble值選擇領取P0 or H0之領號規則，並存入表單item_number欄位](Aras-PLM-Exercise/Client Event/Action/bcs_AutoNumberingAction.js) #item_number #sequence
>Control Event
>>[1.當物件被Click時，取得表單上List物件的值](Aras-PLM-Exercise/Client Event/Control Event/bcs_ClickListValueToText.txt) #List #click

>>[2.在表單上新增按鈕，觸發後新增DCO](Aras-PLM-Exercise/Client Event/Control Event/bcs_DocBusinessToDCO.txt) #click #DCO

>>[3.在表單上新增按鈕，觸發後新增Project](Aras-PLM-Exercise/Client Event/Control Event/bcs_DocBusinessToProject.txt) #click #project

>>[4.當表單上classificatioin值被改變時，改變List控制項的值](Aras-PLM-Exercise/Client Event/Control Event/bcs_iq_projectapply_ClassToFac.txt) #onchange 

>Form Event
>>[1.在表單開啟時，確認物件的狀態是否需要將控制項關閉](Aras-PLM-Exercise/Client Event/Form Event/bcs_ChkAutoNumber.txt) #disable

>>[2.在表單開啟時，載入包含換行字串的內容](Aras-PLM-Exercise/Client Event/Form Event/bcs_Notify_Tech_addContent.txt) #text

##Other
>>[1.Form上的物件詳細資料](Aras-PLM-Exercise/other/item_info.txt) #detail

>>[2.還原資料庫後帳號問題](Aras-PLM-Exercise/other/還原資料庫後帳號問題.txt) #sql
