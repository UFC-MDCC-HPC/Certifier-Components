using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using System.Threading;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{

public class ParallelJob{
		public Thread _thread;
	CertifierOrchestrationElement child;
	public int ret;
	public ParallelJob(CertifierOrchestrationElement child){
		this.child = child;
	}
		public  void run() {
			_thread = new Thread((ThreadStart)delegate() 
			                     {ret = this.child.run();

			});

			_thread.Start ();



		}
	
	}}