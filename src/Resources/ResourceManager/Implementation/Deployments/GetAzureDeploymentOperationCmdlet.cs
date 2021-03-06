﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Implementation
{
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels;
    using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;

    /// <summary>
    /// Gets the deployment operation.
    /// </summary>
    [GenericBreakingChange("A new parameter \"ScopeType\" will be introduced to the cmdlet and will be mandatory when getting deployment opeartions by deployment name. ScopeType will be an enum with four values: ResourceGroup, Subscription, ManagementGroup, Tenant. Adding this parameter allows us to use one cmdlet for all Azure Resource Manager template deployments but still determine the intended level of scope.", "3.0")]
    [Cmdlet(VerbsCommon.Get, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "DeploymentOperation", DefaultParameterSetName = GetAzureDeploymentOperationCmdlet.DeploymentNameParameterSet), OutputType(typeof(PSDeploymentOperation))]
    public class GetAzureDeploymentOperationCmdlet : ResourceManagerCmdletBase
    {
        /// <summary>
        /// The deployment name parameter set.
        /// </summary>
        internal const string DeploymentNameParameterSet = "GetByDeploymentName";

        /// <summary>
        /// The deployment object parameter set.
        /// </summary>
        internal const string DeploymentObjectParameterSet = "GetByDeploymentObject";

        /// <summary>
        /// Gets or sets the deployment name parameter.
        /// </summary>
        [Parameter(ParameterSetName = GetAzureDeploymentOperationCmdlet.DeploymentNameParameterSet, Mandatory = true, HelpMessage = "The deployment name.")]
        [ValidateNotNullOrEmpty]
        public string DeploymentName { get; set; }

        /// <summary>
        /// Gets or sets the deployment operation Id.
        /// </summary>
        [Parameter(ParameterSetName = GetAzureDeploymentOperationCmdlet.DeploymentNameParameterSet, Mandatory = false, HelpMessage = "The deployment operation Id.")]
        public string OperationId { get; set; }

        [Parameter(ParameterSetName = GetAzureDeploymentOperationCmdlet.DeploymentObjectParameterSet, Mandatory = true,
            ValueFromPipeline = true, HelpMessage = "The deployment object.")]
        public PSDeployment DeploymentObject { get; set; }

        public override void ExecuteCmdlet()
        {
            var deploymentName = !string.IsNullOrEmpty(this.DeploymentName) ? this.DeploymentName : this.DeploymentObject.DeploymentName;

            WriteObject(ResourceManagerSdkClient.GetDeploymentOperations(deploymentName, this.OperationId), true);
        }
    }
}
