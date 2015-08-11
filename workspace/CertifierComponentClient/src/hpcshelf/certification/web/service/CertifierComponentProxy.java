package hpcshelf.certification.web.service;

public class CertifierComponentProxy implements hpcshelf.certification.web.service.CertifierComponent {
  private String _endpoint = null;
  private hpcshelf.certification.web.service.CertifierComponent certifierComponent = null;
  
  public CertifierComponentProxy() {
    _initCertifierComponentProxy();
  }
  
  public CertifierComponentProxy(String endpoint) {
    _endpoint = endpoint;
    _initCertifierComponentProxy();
  }
  
  private void _initCertifierComponentProxy() {
    try {
      certifierComponent = (new hpcshelf.certification.web.service.CertifierComponentServiceLocator()).getCertifierComponent();
      if (certifierComponent != null) {
        if (_endpoint != null)
          ((javax.xml.rpc.Stub)certifierComponent)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
        else
          _endpoint = (String)((javax.xml.rpc.Stub)certifierComponent)._getProperty("javax.xml.rpc.service.endpoint.address");
      }
      
    }
    catch (javax.xml.rpc.ServiceException serviceException) {}
  }
  
  public String getEndpoint() {
    return _endpoint;
  }
  
  public void setEndpoint(String endpoint) {
    _endpoint = endpoint;
    if (certifierComponent != null)
      ((javax.xml.rpc.Stub)certifierComponent)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
    
  }
  
  public hpcshelf.certification.web.service.CertifierComponent getCertifierComponent() {
    if (certifierComponent == null)
      _initCertifierComponentProxy();
    return certifierComponent;
  }
  
  public boolean certify(java.lang.String hashAbstractComponent, java.lang.String hashConcreteComponent) throws java.rmi.RemoteException{
    if (certifierComponent == null)
      _initCertifierComponentProxy();
    return certifierComponent.certify(hashAbstractComponent, hashConcreteComponent);
  }
  
  
}