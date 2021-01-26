    inn = this.getInnovator();
    return main(this);
}
private string ConvertNumber(string qStr){
    Decimal de;
    Decimal.TryParse(qStr,System.Globalization.NumberStyles.Any,null,out de);
    return de.ToString();
}

Innovator inn;
private Item main(Item thisItem){
    Item report = inn.newItem("Report","get");
    report.setProperty("name","JPC Multiple Level BOM");
    report = report.apply();
    Item result=inn.newItem();
    if(report.isError()==false){
        string aml = @"<AML>
  <Item type='Part' action='get' select='item_number,name,cn_lifecycle,cn_revision' id='{0}'>
    <Relationships>
      <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_attrition_rate,cn_bom_note,reference_designator'>
        <Relationships>
          <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
            <related_id>
              <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
              </Item>
            </related_id>
          </Item>
        </Relationships>
        <related_id>
          <Item type='Part' select='item_number,name,cn_lifecycle,cn_revision,unit'>
            <Relationships>
              <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_bom_note,cn_attrition_rate,reference_designator'>
                <Relationships>
                  <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
                    <related_id>
                      <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                      </Item>
                    </related_id>
                  </Item>
                </Relationships>
                <related_id>
                  <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                    <Relationships>
                      <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_bom_note,cn_attrition_rate,reference_designator'>
                        <Relationships>
                          <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
                            <related_id>
                              <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                              </Item>
                            </related_id>
                          </Item>
                        </Relationships>
                        <related_id>
                          <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                            <Relationships>
                              <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_attrition_rate,cn_bom_note,reference_designator'>
                                <Relationships>
                                  <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate,cn_bom_note'>
                                    <related_id>
                                      <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                                      </Item>
                                    </related_id>
                                  </Item>
                                </Relationships>
                                <related_id>
                                  <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                                    <Relationships>
                                      <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_attrition_rate,cn_bom_note,reference_designator'>
                                        <Relationships>
                                          <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
                                            <related_id>
                                              <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                                              </Item>
                                            </related_id>
                                          </Item>
                                        </Relationships>
                                        <related_id>
                                          <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                                            <Relationships>
                                              <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_attrition_rate,cn_bom_note,reference_designator'>
                                                <Relationships>
                                                  <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
                                                    <related_id>
                                                      <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                                                      </Item>
                                                    </related_id>
                                                  </Item>
                                                </Relationships>
                                                <related_id>
                                                  <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                                                    <Relationships>
                                                      <Item type='Part BOM' select='sort_order,quantity,related_id(item_number,name),cn_attrition_rate,cn_bom_note,reference_designator'>
                                                        <Relationships>
                                                          <Item action='get' type='BOM Substitute' select='id,related_id,cn_bom_note,cn_sort_order,cn_substitute_quantity,cn_substitute_shrinkrate,cn_substitute_unit,cn_substitute_shrinkrate'>
                                                            <related_id>
                                                              <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision'>
                                                              </Item>
                                                            </related_id>
                                                          </Item>
                                                        </Relationships>
                                                        <related_id>
                                                          <Item type='Part' select='item_number,name, cn_lifecycle,cn_revision,unit'>
                                                          </Item>
                                                        </related_id>
                                                      </Item>
                                                    </Relationships>
                                                  </Item>
                                                </related_id>
                                              </Item>
                                            </Relationships>
                                          </Item>
                                        </related_id>
                                      </Item>
                                    </Relationships>
                                  </Item>
                                </related_id>
                              </Item>
                            </Relationships>
                          </Item>
                        </related_id>
                      </Item>
                    </Relationships>
                  </Item>
                </related_id>
              </Item>
            </Relationships>
          </Item>
        </related_id>
      </Item>
    </Relationships>
  </Item>
</AML>";
        aml = string.Format(aml,thisItem.getID());
        //aml = string.Format(aml,"DE00E0D709364DC1A94939866F816C0F");
        Item part = inn.applyAML(aml);
        Item bom = part.getItemsByXPath("//Item[@type='Part BOM']");
        for(int i=0;i<bom.getItemCount();i++){
            Item b = bom.getItemByIndex(i);
            string qStr = b.getProperty("quantity","0");
            b.setProperty("quantity",ConvertNumber(qStr));
            qStr = b.getProperty("cn_attrition_rate","0");
            b.setProperty("cn_attrition_rate",ConvertNumber(qStr));
        }
        bom = part.getItemsByXPath("//Item[@type='BOM Substitute']");
        for(int i=0;i<bom.getItemCount();i++){
            Item b = bom.getItemByIndex(i);
            string qStr = b.getProperty("cn_substitute_quantity","0");
            b.setProperty("cn_substitute_quantity",ConvertNumber(qStr));
            qStr = b.getProperty("cn_substitute_shrinkrate","0");
            b.setProperty("cn_substitute_shrinkrate",ConvertNumber(qStr));
        }
        result = part;
    }
    return result;