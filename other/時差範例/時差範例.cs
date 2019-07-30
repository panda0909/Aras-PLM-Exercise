I18NSessionContext cntx = this.getInnovator().getI18NSessionContext();
int offset_minutes = cntx.GetCorporateToLocalOffset();
DateTime saveNow = DateTime.Now;
DateTime corporate = saveNow.AddMinutes(offset_minutes);
string zone= cntx.GetTimeZone();
string code = cntx.GetLocale();
string LanguageSuffix = cntx.GetLanguageSuffix();
string LanguageCode = cntx.GetLanguageCode();

string result = @"
TimeZone = {0}
Locale = {1}
LanguageSuffix = {2}
LanguageCode = {3}
CorporateToLocalOffset = {4}
DateTime.Now = {5}
Corporate DateTime = {6}
";
result = string.Format(result,zone,code,LanguageSuffix,LanguageCode
                    ,offset_minutes,saveNow,corporate);
return this.getInnovator().newResult(result.ToString());

/*
<Result>
TimeZone = Taipei Standard Time
Locale = zh-TW
LanguageSuffix = _zt
LanguageCode = zt
CorporateToLocalOffset = 0
DateTime.Now = 7/30/2019 10:05:16 AM
Corporate DateTime = 7/30/2019 10:05:16 AM
</Result>
 */