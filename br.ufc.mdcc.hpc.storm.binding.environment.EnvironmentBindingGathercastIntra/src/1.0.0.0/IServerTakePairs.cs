using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.environment.impl.EnvironmentBindingMbyNTakePairs
{
	public class IServerTakePairs : BaseIServerTakePairs, IServerMbyN<IEnvironmentPortTypeMultiplePartner>
	{
		public override void main()
		{
		}
		public IEnvironmentPortTypeMultiplePartner Service{ set {  }  }
	}
}//
