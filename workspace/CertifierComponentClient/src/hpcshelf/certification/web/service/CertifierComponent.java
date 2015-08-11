/**
 * CertifierComponent.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package hpcshelf.certification.web.service;

public interface CertifierComponent extends java.rmi.Remote {
    public boolean certify(java.lang.String hashAbstractComponent, java.lang.String hashConcreteComponent) throws java.rmi.RemoteException;
}
