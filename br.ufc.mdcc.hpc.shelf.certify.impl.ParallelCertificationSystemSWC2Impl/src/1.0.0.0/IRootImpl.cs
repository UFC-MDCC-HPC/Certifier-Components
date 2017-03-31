using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.certifier.SWC2;
		  
namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl
{
	public class IRootImpl : BaseIRootImpl, IRoot
	{
		public override void main()
		{
			Console.WriteLine ("BEGIN ROOT");
			//SWC2.go ();
			Console.WriteLine ("END ROOT");
		}
	}
}
