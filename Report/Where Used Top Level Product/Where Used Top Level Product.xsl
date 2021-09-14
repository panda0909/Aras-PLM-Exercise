<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:aras="http://www.aras-corp.com"  xmlns:user="http://mycompany.com/mynamespace">
	<xsl:output method="html" omit-xml-declaration="yes" standalone="yes" indent="yes"/>
	<xsl:template match="/">
		<html>
			<head/>
			<style type="text/css" userData="Global">
				.cellSolid {text-align:center;border-top:1px #000000 solid;border-right:1px #000000 solid;border-bottom:1px #000000 solid;border-left:1px #000000 solid;}
				.cellSolidTopRight {border-top:1px #000000 solid;border-right:1px #000000 solid;}
				.cellSolidTopLeft {border-top:1px #000000 solid;border-left:1px #000000 solid;}
				.cellSolidRightBottom {border-right:1px #000000 solid;border-bottom:1px #000000 solid;font-family:helvetica;font-size:8pt;}
				.cellSolidBottomLeft {border-bottom:1px #000000 solid;border-left:1px #000000 solid;}
				.cellSolidTop {border-top:1px #000000 solid;}
				.cellSolidRight {border-right:1px #000000 solid;}
				.cellSolidBottom {border-bottom:1px #000000 solid;}
				.cellSolidLeft {border-left:1px #000000 solid;}
				.cellDashed {border-top:1px #666666 dashed;border-right:1px #666666 dashed;border-bottom:1px #666666 dashed;border-left:1px #666666 dashed;}
				.cellDashedTopRight {border-top:1px #666666 dashed;border-right:1px #666666 dashed;}
				.cellDashedTopLeft {border-top:1px #666666 dashed;border-left:1px #666666 dashed;}
				.cellDashedBottomRight {border-bottom:1px #666666 dashed;border-right:1px #666666 dashed;}
				.cellDashedBottomLeft {border-bottom:1px #666666 dashed;border-left:1px #666666 dashed;}
				.cellDashedTop {border-top:1px #666666 dashed;}
				.cellDashedRight {border-right:1px #666666 dashed;}
				.cellDashedBottom {border-bottom:1px #666666 dashed;}
				.cellDashedLeft {border-left:1px #666666 dashed;}
				.cellHeader {background-color:#CCCCCC;border-top:1px #000000 solid;border-right:1px #000000 solid;border-bottom:1px #000000 solid;padding:2px;text-align:center;text-transform:capitalize;text-align:center;font-family:helvetica;font-weight:bold;font-size:8pt;}
				.cellTitle {border-top:1px #000000 solid;border-right:1px #000000 solid;border-left:1px #000000 solid;padding:2px;text-align:center;text-transform:capitalize;text-align:center;font-family:helvetica;font-weight:bold;font-size:8pt;}
			</style>
            <script>
            <![CDATA[
                function fnExcelReport()
                {
                    var tab_text="<meta http-equiv='content-type' content='application/vnd.ms-excel; charset=UTF-8'/><table border='2px'download='Part'><tr>";
                    var textRange; var j=0;
                    tab = document.getElementById('headerTable'); // id of table

                    for(j = 0 ; j < tab.rows.length ; j++) 
                    {     
                        tab_text=tab_text+tab.rows[j].innerHTML+"</tr>";
                        //tab_text=tab_text+"</tr>";
                    }

                    tab_text=tab_text+"</table>";
                    tab_text= tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
                    tab_text= tab_text.replace(/<img[^>]*>/gi,""); // remove if u want images in your table
                    tab_text= tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

                    var ua = window.navigator.userAgent;
                    var msie = ua.indexOf("MSIE "); 

                    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
                    {
                        txtArea1.document.open("txt/html","replace");
                        txtArea1.document.write(tab_text);
                        txtArea1.document.close();
                        txtArea1.focus(); 
                        sa=txtArea1.document.execCommand("SaveAs",true,"Part");
                    }  
                    else                 //other browser not tested on IE 11
                        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));  

                    return (sa);
                }
				function fnExcelReportAll()
                {
                    var tab_text="<meta http-equiv='content-type' content='application/vnd.ms-excel; charset=UTF-8'/><table border='2px'download='Part'><tr>";
                    var textRange; var j=0;
                    var tables = document.getElementsByClassName('exportTable');
                    for(var t=0;t<tables.length;t++){
                        var tab = tables[t];
                        for(j = 0 ; j < tab.rows.length ; j++) 
                        {     
                            tab_text=tab_text+tab.rows[j].innerHTML+"</tr>";
                        }
                    }
                   

                    tab_text=tab_text+"</table>";
                    tab_text= tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
                    tab_text= tab_text.replace(/<img[^>]*>/gi,""); // remove if u want images in your table
                    tab_text= tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

                    var ua = window.navigator.userAgent;
                    var msie = ua.indexOf("MSIE "); 

                    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
                    {
                        txtArea1.document.open("txt/html","replace");
                        txtArea1.document.write(tab_text);
                        txtArea1.document.close();
                        txtArea1.focus(); 
                        sa=txtArea1.document.execCommand("SaveAs",true,"Part");
                    }  
                    else                 //other browser not tested on IE 11
                        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));  

                    return (sa);
                }
            ]]>
            </script>  
            

			<body topmargin="50" leftmargin="50">
                
				<table border="0" cellspacing="0" cellpadding="0" width="850" id="headerTable" class="exportTable">
					
					<tr valign="bottom">
						<td colspan="6" style="font-family:helvetica;font-size:15pt;color:#DA1943;padding:2px;" align="left" uniqueID="ms__id79">Where Used Top Level Product</td>
						<td colspan="1" style="font-family:helvetica;font-size:10pt;padding:2px;" align="right" uniqueID="ms__id80">
							<button id="btnExport" onclick="fnExcelReportAll();">EXPORT</button>
						</td>
					</tr>
					<tr>
						<td class="cellHeader" uniqueID="ms__id89">Part Number</td>
						<td class="cellHeader" uniqueID="ms__id91">Name</td>
                        <td class="cellHeader" uniqueID="ms__id91">Lifecycle</td>
                        <td class="cellHeader" uniqueID="ms__id91">Revision</td>
                        <td class="cellHeader" uniqueID="ms__id91">Classification</td>
                        <td class="cellHeader" uniqueID="ms__id91">Part Note</td>
                        <td class="cellHeader" uniqueID="ms__id91">Release Date</td>
					</tr>
                    <xsl:call-template name="Levels"/>
				</table>
			</body>
		</html>
	</xsl:template>
    <xsl:template name="Levels">
		<xsl:for-each select="//Item[@type='Part']">
			<xsl:apply-templates mode="PartBOM" select=".">
				
			</xsl:apply-templates>
		</xsl:for-each>
	</xsl:template>
	<xsl:template mode="PartBOM" match="//Item[@type='Part']">
		<tr>
			<td class="cellSolid" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id28">
				<xsl:value-of select="item_number"/>
				<xsl:if test="item_number='' or not(item_number)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
			<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="name"/>
				<xsl:if test="name='' or not(name)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="cn_lifecycle"/>
				<xsl:if test="cn_lifecycle='' or not(cn_lifecycle)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="cn_revision"/>
				<xsl:if test="cn_revision='' or not(cn_revision)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="classification"/>
				<xsl:if test="classification='' or not(classification)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="cn_part_note"/>
				<xsl:if test="cn_part_note='' or not(cn_part_note)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
			<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id27">
				<xsl:value-of select="release_date"/>
				<xsl:if test="release_date='' or not(release_date)">
					<xsl:text> </xsl:text>
				</xsl:if>
			</td>
			
			
		</tr>
	</xsl:template>
</xsl:stylesheet>