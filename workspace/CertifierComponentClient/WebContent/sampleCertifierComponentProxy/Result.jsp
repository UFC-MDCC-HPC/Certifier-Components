<%@page contentType="text/html;charset=UTF-8"%>
<% request.setCharacterEncoding("UTF-8"); %>
<HTML>
<HEAD>
<TITLE>Result</TITLE>
</HEAD>
<BODY>
<H1>Result</H1>

<jsp:useBean id="sampleCertifierComponentProxyid" scope="session" class="hpcshelf.certification.web.service.CertifierComponentProxy" />
<%
if (request.getParameter("endpoint") != null && request.getParameter("endpoint").length() > 0)
sampleCertifierComponentProxyid.setEndpoint(request.getParameter("endpoint"));
%>

<%
String method = request.getParameter("method");
int methodID = 0;
if (method == null) methodID = -1;

if(methodID != -1) methodID = Integer.parseInt(method);
boolean gotMethod = false;

try {
switch (methodID){ 
case 2:
        gotMethod = true;
        java.lang.String getEndpoint2mtemp = sampleCertifierComponentProxyid.getEndpoint();
if(getEndpoint2mtemp == null){
%>
<%=getEndpoint2mtemp %>
<%
}else{
        String tempResultreturnp3 = org.eclipse.jst.ws.util.JspUtils.markup(String.valueOf(getEndpoint2mtemp));
        %>
        <%= tempResultreturnp3 %>
        <%
}
break;
case 5:
        gotMethod = true;
        String endpoint_0id=  request.getParameter("endpoint8");
            java.lang.String endpoint_0idTemp = null;
        if(!endpoint_0id.equals("")){
         endpoint_0idTemp  = endpoint_0id;
        }
        sampleCertifierComponentProxyid.setEndpoint(endpoint_0idTemp);
break;
case 10:
        gotMethod = true;
        hpcshelf.certification.web.service.CertifierComponent getCertifierComponent10mtemp = sampleCertifierComponentProxyid.getCertifierComponent();
if(getCertifierComponent10mtemp == null){
%>
<%=getCertifierComponent10mtemp %>
<%
}else{
        if(getCertifierComponent10mtemp!= null){
        String tempreturnp11 = getCertifierComponent10mtemp.toString();
        %>
        <%=tempreturnp11%>
        <%
        }}
break;
case 13:
        gotMethod = true;
        String hashAbstractComponent_1id=  request.getParameter("hashAbstractComponent16");
            java.lang.String hashAbstractComponent_1idTemp = null;
        if(!hashAbstractComponent_1id.equals("")){
         hashAbstractComponent_1idTemp  = hashAbstractComponent_1id;
        }
        String hashConcreteComponent_2id=  request.getParameter("hashConcreteComponent18");
            java.lang.String hashConcreteComponent_2idTemp = null;
        if(!hashConcreteComponent_2id.equals("")){
         hashConcreteComponent_2idTemp  = hashConcreteComponent_2id;
        }
        boolean certify13mtemp = sampleCertifierComponentProxyid.certify(hashAbstractComponent_1idTemp,hashConcreteComponent_2idTemp);
        String tempResultreturnp14 = org.eclipse.jst.ws.util.JspUtils.markup(String.valueOf(certify13mtemp));
        %>
        <%= tempResultreturnp14 %>
        <%
break;
}
} catch (Exception e) { 
%>
Exception: <%= org.eclipse.jst.ws.util.JspUtils.markup(e.toString()) %>
Message: <%= org.eclipse.jst.ws.util.JspUtils.markup(e.getMessage()) %>
<%
return;
}
if(!gotMethod){
%>
result: N/A
<%
}
%>
</BODY>
</HTML>