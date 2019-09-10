# WebAPITest  
①获取token  
post  http://*/token   
参数[{"key":"grant_type","value":"password"},{"key":"username","value":"Admin"},{"key":"password","value":"123456"}]  
实际环境密码填写加密后的密码（校验逻辑请自己实现），请参照相关代码   
②   
以 api/values作为示例  
Headers   
[{"key":"Authorization","value":"Bearer mMmoOjrsttKtu5zbGbx9voGNlCRLpV4l9gpPVudnoFtHaImTBs3pm6j-rkuRmAcOuB9zHXBgjvJtoENbFSX-uQCNrPTGR1Is48QNLTQwLX5tCdOn_sOgjgTjuSH2_KXgwwTPjs6TkL_-0PuWP7mbJeVZNVMdjSSoUtayNHF7DvxeUfrEPg-9Iw1tSWf_Z8jz92q1NVzDrEuEy3gg-sGRfw","description":""}]   
请求头添加Authorization，值为：Bearer +空格+access_token   
