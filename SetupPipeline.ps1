$rgName = "IoTADF";
	$ADFName ="iotDF";
	$location ="WEST US"
	Login-AzureRMAccount
	New-AzureRMResourceGroup -Name $rgName -Location $location
	 New-AzureRMDataFactory -ResourceGroupName $rgName -Name $ADFName -Location $location
	 $batchName = "IotBatchService";
$batchPoolName = "IOTPool";
New-AzureRmBatchAccount -AccountName $batchName -ResourceGroupName $rgName -Location $location
$keys = Get-AzureRmBatchAccountKeys -AccountName $batchName
New-AzureBatchPool -Name $batchPoolName -BatchContext $keys -VirtualMachineSize "small" -OSFamily "4" -TargetOSVersion "*" -TargetDedicated 1

  $storageName = "SomeStorage" 
   New-AzureRMStorageAccount -Name $storageName -ResourceGroupName  $rgName -Type Standard_LRS -Location $location
   $keys = Get-AzureRmStorageAccountKey -ResourceGroupName $rgName -Name  $storageName

   $df = Get-AzureRMDataFactory -Name uneideladf1 -ResourceGroupName azureiotdatafactory
New-AzureRMDataFactoryLinkedService $df -file ./AzureStorageLinkedService.json -Force
New-AzureRMDataFactoryLinkedService $df -file ./AzureBatchedLinkedService.json -Force
New-AzureRMDataFactoryLinkedService $df -file ./AzureSQLLinkedservice.json -Force
New-AzureRMDataFactoryDataSet $df -file ./IdentityBlobTableDataSet.json -Force
New-AzureRMDataFactoryDataSet $df -file ./inputdataset.json -Force
New-AzureRMDataFactoryDataSet $df -file ./outputdataset.json -Force
New-AzureRMDataFactoryDataSet $df -file ./AzureSQLoutDataSet.json -Force
New-AzureRMDataFactoryDataSet $df -file ./IdentityBlobOutTableDataSet.json -Force
New-AzureRMDataFactoryPipeline $df -file ./CustomExportpipeline.json -Force
New-AzureRMDataFactoryPipeline $df -file ./SQLPipeline.json -Force
New-AzureRMDataFactoryPipeline $df -file ./SQLOutPipeline.json -Force
New-AzureRMDataFactoryPipeline $df -file ./CustomImportpipeline.json -Force