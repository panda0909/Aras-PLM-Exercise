<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:aras="http://www.aras-corp.com" xmlns:user="http://mycompany.com/mynamespace">
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
            ]]>
            </script>          
					 
					
					<body topmargin="50" leftmargin="50">
						
						<table border="0" cellspacing="0" cellpadding="0" width="850" id="headerTable">
							<tr valign="bottom">
								<td colspan="18" style="font-family:helvetica;font-size:15pt;color:#DA1943;padding:2px;" align="left" uniqueID="ms__id79">Mutiple Level BOM </td>
								<td colspan="1" style="font-family:helvetica;font-size:10pt;padding:2px;" align="right" uniqueID="ms__id80">
									<button id="btnExport" onclick="fnExcelReport();">EXPORT</button>
								</td>
							</tr>
							<tr>
								<td class="cellHeader" colspan="8" uniqueID="ms__id86" style="border-left:1px #000000 solid;">Indenture Level</td>
								<td class="cellHeader" uniqueID="ms__id89">Part Number</td>
								<td class="cellHeader" uniqueID="ms__id91">Name</td>
								<td class="cellHeader" uniqueID="ms__id91">Lifecycle</td>
								<td class="cellHeader" uniqueID="ms__id91">Revision</td>
								<td class="cellHeader" uniqueID="ms__id88">SortOrder</td>
								<td class="cellHeader" uniqueID="ms__id88">Quantity</td>
								<td class="cellHeader" uniqueID="ms__id88">Unit</td>
								<td class="cellHeader" uniqueID="ms__id88">Res</td>
								<td class="cellHeader" uniqueID="ms__id88">Subsititue</td>
								<td class="cellHeader" uniqueID="ms__id88">Shrinkrate</td>
								<td class="cellHeader" uniqueID="ms__id88">BOM Note</td>
							</tr>
							<xsl:call-template name="Levels"/>
						</table>
					</body>
				</html>
			</xsl:template>
			<!-- Find the Part Level and pass as the 'Depth' param to the next template -->
			<xsl:template name="Levels">
				<xsl:for-each select="//Item[@type='Part']">
					<xsl:apply-templates mode="PartBOM" select=".">
						<xsl:with-param name="Depth" select="count(ancestor::Relationships)-count(ancestor::Item[@type='BOM Substitute'])"/>
					</xsl:apply-templates>
				</xsl:for-each>
			</xsl:template>
			<xsl:template mode="PartBOM" match="//Item[@type='Part']">
				<xsl:param name="Depth"/>
				<tr>
					<xsl:choose>
						<xsl:when test="$Depth = '0'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id92" align="center" style="border-left:1px #000000 solid;">
								<xsl:value-of select="$Depth"/>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id93">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id94">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id96">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id196">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id60">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id196">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id60">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
						<xsl:when test="$Depth = '1'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id97" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id98" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id99">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id100">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id101">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
						<xsl:when test="$Depth = '2'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id102" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id103">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id104" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id105">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id198">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id62">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
						<xsl:when test="$Depth = '3'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id107" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id108">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id109">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id110" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id111">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id63">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
						<xsl:when test="$Depth = '4'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id112" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id113">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id114">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id115">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id116" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id64">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
						<xsl:when test="$Depth = '5'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id201" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id202">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id203">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id204">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id206">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id205" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
                        <xsl:when test="$Depth = '6'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id201" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id202">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id203">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id204">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id206">
								<xsl:text> </xsl:text>
							</td>
							
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id205" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
						</xsl:when>
                        <xsl:when test="$Depth = '7'">
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id201" style="border-left:1px #000000 solid;">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id202">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id203">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id204">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id206">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
                            <td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id61">
								<xsl:text> </xsl:text>
							</td>
							<td class="cellSolidRightBottom" width="20pt" uniqueID="ms__id205" align="center">
								<xsl:value-of select="$Depth"/>
							</td>
						</xsl:when>
					</xsl:choose>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" uniqueID="ms__id28">
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
					            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="center" uniqueID="ms__id27">
						<xsl:value-of select="cn_lifecycle"/>
						<xsl:if test="cn_lifecycle='' or not(cn_lifecycle)">
							<xsl:text> </xsl:text>
						</xsl:if>
					</td>
					            <td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="center" uniqueID="ms__id27">
						<xsl:value-of select="cn_revision"/>
						<xsl:if test="cn_revision='' or not(cn_revision)">
							<xsl:text> </xsl:text>
						</xsl:if>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="center" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../cn_sort_order != ''">
								<xsl:value-of select="../../../../sort_order"/>
								<xsl:if test="../../../../sort_order='' or not(../../../../sort_order)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="../../sort_order"/>
								<xsl:if test="../../sort_order='' or not(../../sort_order)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="right" uniqueID="ms__id29">
						<xsl:choose>
							<xsl:when test="../../cn_substitute_quantity != '0'">
								<xsl:value-of select="../../cn_substitute_quantity"/>
								<xsl:if test="../../cn_substitute_quantity='' or not(../../cn_substitute_quantity)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="../../quantity != '0'">
								<xsl:value-of select="../../quantity"/>
								<xsl:if test="../../quantity='' or not(../../quantity)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="$Depth = '0'">
								<xsl:text>1</xsl:text>
							</xsl:when>
							<xsl:otherwise>
								<xsl:text>0</xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="center" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../cn_substitute_unit != ''">
								<xsl:value-of select="../../cn_substitute_unit"/>
								<xsl:if test="../../cn_substitute_unit='' or not(../../cn_substitute_unit)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="unit"/>
								<xsl:if test="unit='' or not(unit)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="left" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../reference_designator != ''">
								<xsl:value-of select="../../reference_designator"/>
								<xsl:if test="../../reference_designator='' or not(../../reference_designator)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="../../../../reference_designator != ''">
								<xsl:value-of select="../../../../reference_designator"/>
								
							</xsl:when>
							<xsl:otherwise>
								<xsl:text> </xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="center" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../cn_sort_order != ''">
								<xsl:value-of select="concat('R',(number(../../cn_sort_order)+1))"/>
								<xsl:if test="../../cn_sort_order='' or not(../../cn_sort_order)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="../../Relationships/Item[@type='BOM Substitute']/cn_sort_order != ''">
								<xsl:text>R1</xsl:text>
							</xsl:when>
							<xsl:otherwise>
								<xsl:text> </xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="right" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../cn_substitute_shrinkrate != '0'">
								<xsl:value-of select="../../cn_substitute_shrinkrate"/>
								<xsl:if test="../../cn_substitute_shrinkrate='' or not(../../cn_substitute_shrinkrate)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="../../cn_attrition_rate != '0'">
								<xsl:value-of select="../../cn_attrition_rate"/>
								<xsl:if test="../../cn_attrition_rate='' or not(../../cn_attrition_rate)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:text>0</xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="cellSolidRightBottom" style="font-family:helvetica; font-size:8pt; padding:2px;" align="left" uniqueID="ms__id27">
						<xsl:choose>
							<xsl:when test="../../cn_bom_note != '0'">
								<xsl:value-of select="../../cn_bom_note"/>
								<xsl:if test="../../cn_bom_note='' or not(../../cn_bom_note)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="../../cn_bom_note != '0'">
								<xsl:value-of select="../../cn_bom_note"/>
								<xsl:if test="../../cn_bom_note='' or not(../../cn_bom_note)">
									<xsl:text> </xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:text> </xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</td>
				</tr>
			</xsl:template>
		</xsl:stylesheet>