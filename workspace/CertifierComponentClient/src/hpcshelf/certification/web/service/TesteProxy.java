package hpcshelf.certification.web.service;

public class TesteProxy implements hpcshelf.certification.web.service.Teste {
  private String _endpoint = null;
  private hpcshelf.certification.web.service.Teste teste = null;
  
  public TesteProxy() {
    _initTesteProxy();
  }
  
  public TesteProxy(String endpoint) {
    _endpoint = endpoint;
    _initTesteProxy();
  }
  
  private void _initTesteProxy() {
    try {
      teste = (new hpcshelf.certification.web.service.TesteServiceLocator()).getTeste();
      if (teste != null) {
        if (_endpoint != null)
          ((javax.xml.rpc.Stub)teste)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
        else
          _endpoint = (String)((javax.xml.rpc.Stub)teste)._getProperty("javax.xml.rpc.service.endpoint.address");
      }
      
    }
    catch (javax.xml.rpc.ServiceException serviceException) {}
  }
  
  public String getEndpoint() {
    return _endpoint;
  }
  
  public void setEndpoint(String endpoint) {
    _endpoint = endpoint;
    if (teste != null)
      ((javax.xml.rpc.Stub)teste)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
    
  }
  
  public hpcshelf.certification.web.service.Teste getTeste() {
    if (teste == null)
      _initTesteProxy();
    return teste;
  }
  
  public float subtractValue(float value) throws java.rmi.RemoteException{
    if (teste == null)
      _initTesteProxy();
    return teste.subtractValue(value);
  }
  
  public float addValue(float value) throws java.rmi.RemoteException{
    if (teste == null)
      _initTesteProxy();
    return teste.addValue(value);
  }
  
  
}