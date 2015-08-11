/**
 * TesteService.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package hpcshelf.certification.web.service;

public interface TesteService extends javax.xml.rpc.Service {
    public java.lang.String getTesteAddress();

    public hpcshelf.certification.web.service.Teste getTeste() throws javax.xml.rpc.ServiceException;

    public hpcshelf.certification.web.service.Teste getTeste(java.net.URL portAddress) throws javax.xml.rpc.ServiceException;
}
