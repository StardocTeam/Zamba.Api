# Zamba RRHH Database Documentation

Generated from SQL Server database zambabpm_RRHH. Fill in the **Description** column/section for each table and column.

## Table of Contents

- [dbo.980s](#dbo-980s)
- [dbo.Action_Param](#dbo-actionparam)
- [dbo.Asignar](#dbo-asignar)
- [dbo.Automail](#dbo-automail)
- [dbo.CaracterxNroDoForEach](#dbo-caracterxnrodoforeach)
- [dbo.ChatHistories](#dbo-chathistories)
- [dbo.ChatPeoples](#dbo-chatpeoples)
- [dbo.Chats](#dbo-chats)
- [dbo.ChatUsers](#dbo-chatusers)
- [dbo.CheckListEmision](#dbo-checklistemision)
- [dbo.checklistreclamo](#dbo-checklistreclamo)
- [dbo.CollaborativeDocuments](#dbo-collaborativedocuments)
- [dbo.CollaborativeSessions](#dbo-collaborativesessions)
- [dbo.ContadoresWf](#dbo-contadoreswf)
- [dbo.DatosStores](#dbo-datosstores)
- [dbo.Default_Search](#dbo-defaultsearch)
- [dbo.DISK_GROUP](#dbo-diskgroup)
- [dbo.DISK_GROUP_R_DISK_VOLUME](#dbo-diskgrouprdiskvolume)
- [dbo.DISK_VOLUME](#dbo-diskvolume)
- [dbo.DOC_INDEX](#dbo-docindex)
- [dbo.DOC_NOTES](#dbo-docnotes)
- [dbo.DOC_RESTRICTION_R_USER](#dbo-docrestrictionruser)
- [dbo.DOC_RESTRICTIONS](#dbo-docrestrictions)
- [dbo.DOC_T156129](#dbo-doct156129)
- [dbo.DOC_TYPE](#dbo-doctype)
- [dbo.DOC_TYPE_GROUP](#dbo-doctypegroup)
- [dbo.DOC_TYPE_R_DOC_TYPE](#dbo-doctyperdoctype)
- [dbo.DOC_TYPE_R_DOC_TYPE_GROUP](#dbo-doctyperdoctypegroup)
- [dbo.DocumentChanges](#dbo-documentchanges)
- [dbo.DocumentKeyIndexs](#dbo-documentkeyindexs)
- [dbo.DocumentLabels](#dbo-documentlabels)
- [dbo.DocumentsIndexs](#dbo-documentsindexs)
- [dbo.DocumentsToInsertStatus](#dbo-documentstoinsertstatus)
- [dbo.DocumentsUpdateStatus](#dbo-documentsupdatestatus)
- [dbo.DocumentUpdateIndexs](#dbo-documentupdateindexs)
- [dbo.DocumentVersions](#dbo-documentversions)
- [dbo.ESTREG](#dbo-estreg)
- [dbo.EXCEP](#dbo-excep)
- [dbo.Fechas](#dbo-fechas)
- [dbo.FILE_COMPRESSION_AUDIT](#dbo-filecompressionaudit)
- [dbo.FRMNOTES](#dbo-frmnotes)
- [dbo.group_r_group](#dbo-grouprgroup)
- [dbo.GrupoPorNivel](#dbo-grupopornivel)
- [dbo.HelpApplications](#dbo-helpapplications)
- [dbo.Hoja1$](#dbo-hoja1)
- [dbo.ILMClientSet](#dbo-ilmclientset)
- [dbo.Images](#dbo-images)
- [dbo.Images_Type](#dbo-imagestype)
- [dbo.INDEX_LINK](#dbo-indexlink)
- [dbo.INDEX_LINK_INFO](#dbo-indexlinkinfo)
- [dbo.INDEX_R_DOC_TYPE](#dbo-indexrdoctype)
- [dbo.INDEX_R_INDEX_TYPE](#dbo-indexrindextype)
- [dbo.INDEX_TYPE](#dbo-indextype)
- [dbo.IP_FOLDER](#dbo-ipfolder)
- [dbo.IP_FOLDER_PREPROCESS](#dbo-ipfolderpreprocess)
- [dbo.IP_FOLDERBACKUP](#dbo-ipfolderbackup)
- [dbo.IP_FOLDERCONF](#dbo-ipfolderconf)
- [dbo.IP_HST](#dbo-iphst)
- [dbo.IP_INDEX](#dbo-ipindex)
- [dbo.IP_PREPROCESS](#dbo-ippreprocess)
- [dbo.IP_PROCESSTASK](#dbo-ipprocesstask)
- [dbo.IP_RESULTS](#dbo-ipresults)
- [dbo.IP_TASK](#dbo-iptask)
- [dbo.IP_TGRP](#dbo-iptgrp)
- [dbo.IP_TYPE](#dbo-iptype)
- [dbo.IPJOB_LIST](#dbo-ipjoblist)
- [dbo.IPJOB_R_IPJOB_LIST](#dbo-ipjobripjoblist)
- [dbo.LCK](#dbo-lck)
- [dbo.LIC](#dbo-lic)
- [dbo.LICTYPE](#dbo-lictype)
- [dbo.LIQUIDADORESEXTERNOS](#dbo-liquidadoresexternos)
- [dbo.LOTUSINDEXMAPPING](#dbo-lotusindexmapping)
- [dbo.LSTDID](#dbo-lstdid)
- [dbo.LSTFNAME](#dbo-lstfname)
- [dbo.MESSAGE](#dbo-message)
- [dbo.MODULE_RIGHTS](#dbo-modulerights)
- [dbo.MODULESRIGHTS](#dbo-modulesrights)
- [dbo.MSG_ATTACH](#dbo-msgattach)
- [dbo.MSG_DEST](#dbo-msgdest)
- [dbo.MSG_DEST_TYPE](#dbo-msgdesttype)
- [dbo.NOTESINDEX_R_DOC_INDEX](#dbo-notesindexrdocindex)
- [dbo.OBJECTID](#dbo-objectid)
- [dbo.OBJECTTYPES](#dbo-objecttypes)
- [dbo.OBJLASTID](#dbo-objlastid)
- [dbo.OBJLASTIDBKP](#dbo-objlastidbkp)
- [dbo.OUT_MAP_ATTACH_IDS](#dbo-outmapattachids)
- [dbo.OUT_MAP_DOC_TYPE](#dbo-outmapdoctype)
- [dbo.OUT_MAP_FOLDER](#dbo-outmapfolder)
- [dbo.P_HST](#dbo-phst)
- [dbo.PreProcessIDs](#dbo-preprocessids)
- [dbo.PRJ_R_O](#dbo-prjro)
- [dbo.PRJ_TBL](#dbo-prjtbl)
- [dbo.PRO](#dbo-pro)
- [dbo.RemoteInsert](#dbo-remoteinsert)
- [dbo.remoteinsert_relations](#dbo-remoteinsertrelations)
- [dbo.RemoteServers](#dbo-remoteservers)
- [dbo.RemoteUpdate](#dbo-remoteupdate)
- [dbo.RemovedActivity](#dbo-removedactivity)
- [dbo.ReportBuilder](#dbo-reportbuilder)
- [dbo.ReportBuilder_ChartOption](#dbo-reportbuilderchartoption)
- [dbo.reportbuilder_linkOption](#dbo-reportbuilderlinkoption)
- [dbo.ReportDecodeValues](#dbo-reportdecodevalues)
- [dbo.Reporte_General](#dbo-reportegeneral)
- [dbo.REPORTE_ID](#dbo-reporteid)
- [dbo.ReporteGeneral_Rule](#dbo-reportegeneralrule)
- [dbo.RequestAction](#dbo-requestaction)
- [dbo.RequestActionExecution](#dbo-requestactionexecution)
- [dbo.RequestActionRules](#dbo-requestactionrules)
- [dbo.RequestActionTasks](#dbo-requestactiontasks)
- [dbo.RequestActionUsers](#dbo-requestactionusers)
- [dbo.Resultados](#dbo-resultados)
- [dbo.RIGHTSTYPE](#dbo-rightstype)
- [dbo.Schedule](#dbo-schedule)
- [dbo.Secciones](#dbo-secciones)
- [dbo.Security](#dbo-security)
- [dbo.SLST_S156664](#dbo-slsts156664)
- [dbo.Tabla](#dbo-tabla)
- [dbo.Tmp_Reporte_General](#dbo-tmpreportegeneral)
- [dbo.TypesofRules](#dbo-typesofrules)
- [dbo.UCM](#dbo-ucm)
- [dbo.UCM_HST](#dbo-ucmhst)
- [dbo.UCMCLIENTSSet](#dbo-ucmclientsset)
- [dbo.USER_HST](#dbo-userhst)
- [dbo.USERADMIN](#dbo-useradmin)
- [dbo.USERPARAM](#dbo-userparam)
- [dbo.USR_R_FORM](#dbo-usrrform)
- [dbo.USR_R_GROUP](#dbo-usrrgroup)
- [dbo.USR_R_OPENTASK](#dbo-usrropentask)
- [dbo.USR_RIGHTS](#dbo-usrrights)
- [dbo.Usr_Seccion](#dbo-usrseccion)
- [dbo.UsrData](#dbo-usrdata)
- [dbo.USRGROUP](#dbo-usrgroup)
- [dbo.USRNOTES](#dbo-usrnotes)
- [dbo.USRTABLE](#dbo-usrtable)
- [dbo.VERREG](#dbo-verreg)
- [dbo.WaitForDocument](#dbo-waitfordocument)
- [dbo.WaitIndex](#dbo-waitindex)
- [dbo.WF_DT](#dbo-wfdt)
- [dbo.WF_Frms](#dbo-wffrms)
- [dbo.WF_Opt_Sistema](#dbo-wfoptsistema)
- [dbo.WF_SVMG_ACTION_DESC](#dbo-wfsvmgactiondesc)
- [dbo.WF_SVMG_ACTIONS](#dbo-wfsvmgactions)
- [dbo.WF_SVMG_OPT](#dbo-wfsvmgopt)
- [dbo.WF_SVMG_SERVICES](#dbo-wfsvmgservices)
- [dbo.WFContexts](#dbo-wfcontexts)
- [dbo.WFDocument](#dbo-wfdocument)
- [dbo.WFFaces](#dbo-wffaces)
- [dbo.WFIndexAndVariable](#dbo-wfindexandvariable)
- [dbo.WFIndexAndVariable_r_WFRules](#dbo-wfindexandvariablerwfrules)
- [dbo.WFIndexAndVariableConfig](#dbo-wfindexandvariableconfig)
- [dbo.WFRuleParamItems](#dbo-wfruleparamitems)
- [dbo.WFRules](#dbo-wfrules)
- [dbo.WFRULES_R_WFRULESTATES](#dbo-wfrulesrwfrulestates)
- [dbo.WFRulesHst](#dbo-wfruleshst)
- [dbo.WFRULESTATES](#dbo-wfrulestates)
- [dbo.WFStateHst](#dbo-wfstatehst)
- [dbo.WFStep](#dbo-wfstep)
- [dbo.WFStepHst](#dbo-wfstephst)
- [dbo.WFStepOpt](#dbo-wfstepopt)
- [dbo.WfStepPerformance](#dbo-wfstepperformance)
- [dbo.WFStepStates](#dbo-wfstepstates)
- [dbo.WFTask_States](#dbo-wftaskstates)
- [dbo.WFWorkflow](#dbo-wfworkflow)
- [dbo.Z_GroupToNotify](#dbo-zgrouptonotify)
- [dbo.Z_ME_P](#dbo-zmep)
- [dbo.Z_ME_PD](#dbo-zmepd)
- [dbo.Z_ME_PM](#dbo-zmepm)
- [dbo.Z_Publish](#dbo-zpublish)
- [dbo.Z_SendToUser](#dbo-zsendtouser)
- [dbo.ZABMTables](#dbo-zabmtables)
- [dbo.ZambaExport](#dbo-zambaexport)
- [dbo.ZambaExportIndexes](#dbo-zambaexportindexes)
- [dbo.ZambaWFService](#dbo-zambawfservice)
- [dbo.ZAPP](#dbo-zapp)
- [dbo.Zaprob](#dbo-zaprob)
- [dbo.ZBarCode](#dbo-zbarcode)
- [dbo.ZBarcodeComplete](#dbo-zbarcodecomplete)
- [dbo.Zbinary](#dbo-zbinary)
- [dbo.ZButtons](#dbo-zbuttons)
- [dbo.ZButtonsAndGroups](#dbo-zbuttonsandgroups)
- [dbo.ZButtonsPlace](#dbo-zbuttonsplace)
- [dbo.ZButtonsType](#dbo-zbuttonstype)
- [dbo.ZCARATULAS](#dbo-zcaratulas)
- [dbo.ZCARATULAS_STATES](#dbo-zcaratulasstates)
- [dbo.ZComment](#dbo-zcomment)
- [dbo.ZComparators](#dbo-zcomparators)
- [dbo.ZdbtoxmlActions](#dbo-zdbtoxmlactions)
- [dbo.ZDocReads](#dbo-zdocreads)
- [dbo.ZDocRelations](#dbo-zdocrelations)
- [dbo.ZDocTypeRights](#dbo-zdoctyperights)
- [dbo.ZErrorReportAttachments](#dbo-zerrorreportattachments)
- [dbo.ZErrorReports](#dbo-zerrorreports)
- [dbo.ZErrorReportStates](#dbo-zerrorreportstates)
- [dbo.ZExportControl](#dbo-zexportcontrol)
- [dbo.ZExportErrors](#dbo-zexporterrors)
- [dbo.Zext_zfrms](#dbo-zextzfrms)
- [dbo.zfilters](#dbo-zfilters)
- [dbo.ZForum](#dbo-zforum)
- [dbo.ZFORUM_R_DOC](#dbo-zforumrdoc)
- [dbo.ZForum_R_USR](#dbo-zforumrusr)
- [dbo.ZForumAttachBlob](#dbo-zforumattachblob)
- [dbo.ZForumAttachs](#dbo-zforumattachs)
- [dbo.ZForumMails](#dbo-zforummails)
- [dbo.ZFrmDesc](#dbo-zfrmdesc)
- [dbo.zFrmDynamicForms](#dbo-zfrmdynamicforms)
- [dbo.ZFrmIndexsDesc](#dbo-zfrmindexsdesc)
- [dbo.ZFrms](#dbo-zfrms)
- [dbo.ZFrms_Actions](#dbo-zfrmsactions)
- [dbo.zfrms_AdditionalFiles](#dbo-zfrmsadditionalfiles)
- [dbo.ZFrms_Conditions](#dbo-zfrmsconditions)
- [dbo.ZFRMSBLOB](#dbo-zfrmsblob)
- [dbo.ZFrmsRequirements](#dbo-zfrmsrequirements)
- [dbo.ZI](#dbo-zi)
- [dbo.zicons](#dbo-zicons)
- [dbo.ZIndexAutoincremental](#dbo-zindexautoincremental)
- [dbo.ZindexerState](#dbo-zindexerstate)
- [dbo.ZIndexHierarchyKey](#dbo-zindexhierarchykey)
- [dbo.ZIndexReference](#dbo-zindexreference)
- [dbo.ZIndexReferenceKeys](#dbo-zindexreferencekeys)
- [dbo.ZIR](#dbo-zir)
- [dbo.ZLS](#dbo-zls)
- [dbo.ZLSQ](#dbo-zlsq)
- [dbo.ZMachineConfig](#dbo-zmachineconfig)
- [dbo.ZMAIL_HISTORY](#dbo-zmailhistory)
- [dbo.ZMailConfig](#dbo-zmailconfig)
- [dbo.ZMB_SCHEDULER](#dbo-zmbscheduler)
- [dbo.ZMB_WF_Consultas_Documentacion](#dbo-zmbwfconsultasdocumentacion)
- [dbo.ZMOD](#dbo-zmod)
- [dbo.ZNetronShapes](#dbo-znetronshapes)
- [dbo.ZNews](#dbo-znews)
- [dbo.ZNewsUsers](#dbo-znewsusers)
- [dbo.ZObjCount](#dbo-zobjcount)
- [dbo.ZOBS_139103_149657](#dbo-zobs139103149657)
- [dbo.Zopt](#dbo-zopt)
- [dbo.ZPAQ](#dbo-zpaq)
- [dbo.ZparamsItemMail](#dbo-zparamsitemmail)
- [dbo.ZPRINT](#dbo-zprint)
- [dbo.ZQCOLUMNS](#dbo-zqcolumns)
- [dbo.ZQKEYS](#dbo-zqkeys)
- [dbo.ZQSENTENCE](#dbo-zqsentence)
- [dbo.ZQUERYNAME](#dbo-zqueryname)
- [dbo.ZRelations](#dbo-zrelations)
- [dbo.ZRelationsIndexs](#dbo-zrelationsindexs)
- [dbo.ZRuleOptBase](#dbo-zruleoptbase)
- [dbo.ZSCripts](#dbo-zscripts)
- [dbo.ZSearchDictionary](#dbo-zsearchdictionary)
- [dbo.ZSearchValues](#dbo-zsearchvalues)
- [dbo.ZSearchValues_DT](#dbo-zsearchvaluesdt)
- [dbo.ZSecciones](#dbo-zsecciones)
- [dbo.ZSecOption](#dbo-zsecoption)
- [dbo.ZSer](#dbo-zser)
- [dbo.ZService](#dbo-zservice)
- [dbo.ZServiceActions](#dbo-zserviceactions)
- [dbo.ZServiceActionsDesc](#dbo-zserviceactionsdesc)
- [dbo.ZServiceDates](#dbo-zservicedates)
- [dbo.ZServiceOptions](#dbo-zserviceoptions)
- [dbo.ZServiceTypes](#dbo-zservicetypes)
- [dbo.zservidores](#dbo-zservidores)
- [dbo.ZSiteMap](#dbo-zsitemap)
- [dbo.ZSO](#dbo-zso)
- [dbo.Zss](#dbo-zss)
- [dbo.ZTaskStatus](#dbo-ztaskstatus)
- [dbo.ZTC_CT](#dbo-ztcct)
- [dbo.ZTC_EX](#dbo-ztcex)
- [dbo.ZTC_RS](#dbo-ztcrs)
- [dbo.ZTC_ST](#dbo-ztcst)
- [dbo.ZTC_TS](#dbo-ztcts)
- [dbo.ZTC_ZG](#dbo-ztczg)
- [dbo.ZTempl](#dbo-ztempl)
- [dbo.ZThumb](#dbo-zthumb)
- [dbo.ZTRACE](#dbo-ztrace)
- [dbo.Ztype_Zfrms](#dbo-ztypezfrms)
- [dbo.ZUC_UCS](#dbo-zucucs)
- [dbo.ZUD](#dbo-zud)
- [dbo.ZUDT](#dbo-zudt)
- [dbo.ZUSER_OR_GROUP](#dbo-zuserorgroup)
- [dbo.ZUSER_OR_GROUPBKP](#dbo-zuserorgroupbkp)
- [dbo.zuserconfig](#dbo-zuserconfig)
- [dbo.ZV_LEGAJOS_PLAYFT_DOCUMENTOS](#dbo-zvlegajosplayftdocumentos)
- [dbo.ZVariables](#dbo-zvariables)
- [dbo.Zvars](#dbo-zvars)
- [dbo.ZVCR](#dbo-zvcr)
- [dbo.ZVerConfig](#dbo-zverconfig)
- [dbo.ZVerEv](#dbo-zverev)
- [dbo.ZVerEvents](#dbo-zverevents)
- [dbo.ZVIR](#dbo-zvir)
- [dbo.ZVW](#dbo-zvw)
- [dbo.ZWFI](#dbo-zwfi)
- [dbo.ZWFII](#dbo-zwfii)
- [dbo.Zwfopt](#dbo-zwfopt)
- [zambabpm_RRHH.Areas_UsrGroup](#zambabpmrrhh-areasusrgroup)
- [zambabpm_RRHH.BKP_doc_t146_020626](#zambabpmrrhh-bkpdoct146020626)
- [zambabpm_RRHH.BKP_UCM_220526](#zambabpmrrhh-bkpucm220526)
- [zambabpm_RRHH.BKP_UCM_240626](#zambabpmrrhh-bkpucm240626)
- [zambabpm_RRHH.BKP_Zopt_240626](#zambabpmrrhh-bkpzopt240626)
- [zambabpm_RRHH.BKP_zuserconfig_270526](#zambabpmrrhh-bkpzuserconfig270526)
- [zambabpm_RRHH.CalendarEvents](#zambabpmrrhh-calendarevents)
- [zambabpm_RRHH.CarouselConfig](#zambabpmrrhh-carouselconfig)
- [zambabpm_RRHH.CarouselContent](#zambabpmrrhh-carouselcontent)
- [zambabpm_RRHH.ChartConfig](#zambabpmrrhh-chartconfig)
- [zambabpm_RRHH.ChartContainer](#zambabpmrrhh-chartcontainer)
- [zambabpm_RRHH.ChartType](#zambabpmrrhh-charttype)
- [zambabpm_RRHH.DashboardUserConfig](#zambabpmrrhh-dashboarduserconfig)
- [zambabpm_RRHH.DashboardUsers](#zambabpmrrhh-dashboardusers)
- [zambabpm_RRHH.Departments](#zambabpmrrhh-departments)
- [zambabpm_RRHH.DOC_B101](#zambabpmrrhh-docb101)
- [zambabpm_RRHH.DOC_B102](#zambabpmrrhh-docb102)
- [zambabpm_RRHH.DOC_B103](#zambabpmrrhh-docb103)
- [zambabpm_RRHH.DOC_B104](#zambabpmrrhh-docb104)
- [zambabpm_RRHH.DOC_B105](#zambabpmrrhh-docb105)
- [zambabpm_RRHH.DOC_B107](#zambabpmrrhh-docb107)
- [zambabpm_RRHH.DOC_B108](#zambabpmrrhh-docb108)
- [zambabpm_RRHH.DOC_B109](#zambabpmrrhh-docb109)
- [zambabpm_RRHH.DOC_B110](#zambabpmrrhh-docb110)
- [zambabpm_RRHH.DOC_B111](#zambabpmrrhh-docb111)
- [zambabpm_RRHH.DOC_B113](#zambabpmrrhh-docb113)
- [zambabpm_RRHH.DOC_B114](#zambabpmrrhh-docb114)
- [zambabpm_RRHH.DOC_B118](#zambabpmrrhh-docb118)
- [zambabpm_RRHH.DOC_B119](#zambabpmrrhh-docb119)
- [zambabpm_RRHH.DOC_B120](#zambabpmrrhh-docb120)
- [zambabpm_RRHH.DOC_B121](#zambabpmrrhh-docb121)
- [zambabpm_RRHH.DOC_B122](#zambabpmrrhh-docb122)
- [zambabpm_RRHH.DOC_B123](#zambabpmrrhh-docb123)
- [zambabpm_RRHH.DOC_B124](#zambabpmrrhh-docb124)
- [zambabpm_RRHH.DOC_B125](#zambabpmrrhh-docb125)
- [zambabpm_RRHH.DOC_B126](#zambabpmrrhh-docb126)
- [zambabpm_RRHH.DOC_B129](#zambabpmrrhh-docb129)
- [zambabpm_RRHH.DOC_B130](#zambabpmrrhh-docb130)
- [zambabpm_RRHH.DOC_B131](#zambabpmrrhh-docb131)
- [zambabpm_RRHH.DOC_B132](#zambabpmrrhh-docb132)
- [zambabpm_RRHH.DOC_B134](#zambabpmrrhh-docb134)
- [zambabpm_RRHH.DOC_B135](#zambabpmrrhh-docb135)
- [zambabpm_RRHH.DOC_B136](#zambabpmrrhh-docb136)
- [zambabpm_RRHH.DOC_B137](#zambabpmrrhh-docb137)
- [zambabpm_RRHH.DOC_B138](#zambabpmrrhh-docb138)
- [zambabpm_RRHH.DOC_B139](#zambabpmrrhh-docb139)
- [zambabpm_RRHH.DOC_B140](#zambabpmrrhh-docb140)
- [zambabpm_RRHH.DOC_B142](#zambabpmrrhh-docb142)
- [zambabpm_RRHH.DOC_B143](#zambabpmrrhh-docb143)
- [zambabpm_RRHH.DOC_B144](#zambabpmrrhh-docb144)
- [zambabpm_RRHH.DOC_B145](#zambabpmrrhh-docb145)
- [zambabpm_RRHH.DOC_B146](#zambabpmrrhh-docb146)
- [zambabpm_RRHH.DOC_B148](#zambabpmrrhh-docb148)
- [zambabpm_RRHH.DOC_B149](#zambabpmrrhh-docb149)
- [zambabpm_RRHH.DOC_B150](#zambabpmrrhh-docb150)
- [zambabpm_RRHH.DOC_B151](#zambabpmrrhh-docb151)
- [zambabpm_RRHH.DOC_B152](#zambabpmrrhh-docb152)
- [zambabpm_RRHH.DOC_I101](#zambabpmrrhh-doci101)
- [zambabpm_RRHH.DOC_I102](#zambabpmrrhh-doci102)
- [zambabpm_RRHH.DOC_I103](#zambabpmrrhh-doci103)
- [zambabpm_RRHH.DOC_I104](#zambabpmrrhh-doci104)
- [zambabpm_RRHH.DOC_I105](#zambabpmrrhh-doci105)
- [zambabpm_RRHH.DOC_I107](#zambabpmrrhh-doci107)
- [zambabpm_RRHH.DOC_I108](#zambabpmrrhh-doci108)
- [zambabpm_RRHH.DOC_I109](#zambabpmrrhh-doci109)
- [zambabpm_RRHH.DOC_I110](#zambabpmrrhh-doci110)
- [zambabpm_RRHH.DOC_I111](#zambabpmrrhh-doci111)
- [zambabpm_RRHH.DOC_I113](#zambabpmrrhh-doci113)
- [zambabpm_RRHH.DOC_I114](#zambabpmrrhh-doci114)
- [zambabpm_RRHH.DOC_I118](#zambabpmrrhh-doci118)
- [zambabpm_RRHH.DOC_I119](#zambabpmrrhh-doci119)
- [zambabpm_RRHH.DOC_I120](#zambabpmrrhh-doci120)
- [zambabpm_RRHH.DOC_I121](#zambabpmrrhh-doci121)
- [zambabpm_RRHH.DOC_I122](#zambabpmrrhh-doci122)
- [zambabpm_RRHH.DOC_I123](#zambabpmrrhh-doci123)
- [zambabpm_RRHH.DOC_I124](#zambabpmrrhh-doci124)
- [zambabpm_RRHH.DOC_I125](#zambabpmrrhh-doci125)
- [zambabpm_RRHH.DOC_I126](#zambabpmrrhh-doci126)
- [zambabpm_RRHH.DOC_I129](#zambabpmrrhh-doci129)
- [zambabpm_RRHH.DOC_I130](#zambabpmrrhh-doci130)
- [zambabpm_RRHH.DOC_I131](#zambabpmrrhh-doci131)
- [zambabpm_RRHH.DOC_I132](#zambabpmrrhh-doci132)
- [zambabpm_RRHH.DOC_I134](#zambabpmrrhh-doci134)
- [zambabpm_RRHH.DOC_I135](#zambabpmrrhh-doci135)
- [zambabpm_RRHH.DOC_I136](#zambabpmrrhh-doci136)
- [zambabpm_RRHH.DOC_I137](#zambabpmrrhh-doci137)
- [zambabpm_RRHH.DOC_I138](#zambabpmrrhh-doci138)
- [zambabpm_RRHH.DOC_I139](#zambabpmrrhh-doci139)
- [zambabpm_RRHH.DOC_I140](#zambabpmrrhh-doci140)
- [zambabpm_RRHH.DOC_I142](#zambabpmrrhh-doci142)
- [zambabpm_RRHH.DOC_I143](#zambabpmrrhh-doci143)
- [zambabpm_RRHH.DOC_I144](#zambabpmrrhh-doci144)
- [zambabpm_RRHH.DOC_I145](#zambabpmrrhh-doci145)
- [zambabpm_RRHH.DOC_I146](#zambabpmrrhh-doci146)
- [zambabpm_RRHH.DOC_I148](#zambabpmrrhh-doci148)
- [zambabpm_RRHH.DOC_I149](#zambabpmrrhh-doci149)
- [zambabpm_RRHH.DOC_I150](#zambabpmrrhh-doci150)
- [zambabpm_RRHH.DOC_I151](#zambabpmrrhh-doci151)
- [zambabpm_RRHH.DOC_I152](#zambabpmrrhh-doci152)
- [zambabpm_RRHH.DOC_T101](#zambabpmrrhh-doct101)
- [zambabpm_RRHH.DOC_T102](#zambabpmrrhh-doct102)
- [zambabpm_RRHH.DOC_T103](#zambabpmrrhh-doct103)
- [zambabpm_RRHH.DOC_T104](#zambabpmrrhh-doct104)
- [zambabpm_RRHH.DOC_T105](#zambabpmrrhh-doct105)
- [zambabpm_RRHH.DOC_T107](#zambabpmrrhh-doct107)
- [zambabpm_RRHH.DOC_T108](#zambabpmrrhh-doct108)
- [zambabpm_RRHH.DOC_T109](#zambabpmrrhh-doct109)
- [zambabpm_RRHH.DOC_T110](#zambabpmrrhh-doct110)
- [zambabpm_RRHH.DOC_T111](#zambabpmrrhh-doct111)
- [zambabpm_RRHH.DOC_T113](#zambabpmrrhh-doct113)
- [zambabpm_RRHH.DOC_T114](#zambabpmrrhh-doct114)
- [zambabpm_RRHH.DOC_T118](#zambabpmrrhh-doct118)
- [zambabpm_RRHH.DOC_T119](#zambabpmrrhh-doct119)
- [zambabpm_RRHH.DOC_T120](#zambabpmrrhh-doct120)
- [zambabpm_RRHH.DOC_T121](#zambabpmrrhh-doct121)
- [zambabpm_RRHH.DOC_T122](#zambabpmrrhh-doct122)
- [zambabpm_RRHH.DOC_T123](#zambabpmrrhh-doct123)
- [zambabpm_RRHH.DOC_T124](#zambabpmrrhh-doct124)
- [zambabpm_RRHH.DOC_T125](#zambabpmrrhh-doct125)
- [zambabpm_RRHH.DOC_T126](#zambabpmrrhh-doct126)
- [zambabpm_RRHH.DOC_T129](#zambabpmrrhh-doct129)
- [zambabpm_RRHH.DOC_T130](#zambabpmrrhh-doct130)
- [zambabpm_RRHH.DOC_T131](#zambabpmrrhh-doct131)
- [zambabpm_RRHH.DOC_T132](#zambabpmrrhh-doct132)
- [zambabpm_RRHH.DOC_T134](#zambabpmrrhh-doct134)
- [zambabpm_RRHH.DOC_T135](#zambabpmrrhh-doct135)
- [zambabpm_RRHH.DOC_T136](#zambabpmrrhh-doct136)
- [zambabpm_RRHH.DOC_T137](#zambabpmrrhh-doct137)
- [zambabpm_RRHH.DOC_T138](#zambabpmrrhh-doct138)
- [zambabpm_RRHH.DOC_T139](#zambabpmrrhh-doct139)
- [zambabpm_RRHH.DOC_T140](#zambabpmrrhh-doct140)
- [zambabpm_RRHH.DOC_T142](#zambabpmrrhh-doct142)
- [zambabpm_RRHH.DOC_T143](#zambabpmrrhh-doct143)
- [zambabpm_RRHH.DOC_T144](#zambabpmrrhh-doct144)
- [zambabpm_RRHH.DOC_T145](#zambabpmrrhh-doct145)
- [zambabpm_RRHH.DOC_T146](#zambabpmrrhh-doct146)
- [zambabpm_RRHH.DOC_T148](#zambabpmrrhh-doct148)
- [zambabpm_RRHH.DOC_T149](#zambabpmrrhh-doct149)
- [zambabpm_RRHH.DOC_T150](#zambabpmrrhh-doct150)
- [zambabpm_RRHH.DOC_T151](#zambabpmrrhh-doct151)
- [zambabpm_RRHH.DOC_T152](#zambabpmrrhh-doct152)
- [zambabpm_RRHH.DocumentsFromPath](#zambabpmrrhh-documentsfrompath)
- [zambabpm_RRHH.DocumentsFromPathLevels](#zambabpmrrhh-documentsfrompathlevels)
- [zambabpm_RRHH.EncriptedFiles](#zambabpmrrhh-encriptedfiles)
- [zambabpm_RRHH.EventLogDetails](#zambabpmrrhh-eventlogdetails)
- [zambabpm_RRHH.ILST_I344](#zambabpmrrhh-ilsti344)
- [zambabpm_RRHH.Paises](#zambabpmrrhh-paises)
- [zambabpm_RRHH.pendingverificationusers](#zambabpmrrhh-pendingverificationusers)
- [zambabpm_RRHH.ReportCategories](#zambabpmrrhh-reportcategories)
- [zambabpm_RRHH.ReportChartConfig](#zambabpmrrhh-reportchartconfig)
- [zambabpm_RRHH.ReportChartContainer](#zambabpmrrhh-reportchartcontainer)
- [zambabpm_RRHH.reportDescription](#zambabpmrrhh-reportdescription)
- [zambabpm_RRHH.ReportDescription_Backup13032026](#zambabpmrrhh-reportdescriptionbackup13032026)
- [zambabpm_RRHH.reporteGeneral_ReportCategories](#zambabpmrrhh-reportegeneralreportcategories)
- [zambabpm_RRHH.reporteGeneral_reportDescription](#zambabpmrrhh-reportegeneralreportdescription)
- [zambabpm_RRHH.resetpasswordtokens](#zambabpmrrhh-resetpasswordtokens)
- [zambabpm_RRHH.RH_SidebarMenuItem](#zambabpmrrhh-rhsidebarmenuitem)
- [zambabpm_RRHH.Rol](#zambabpmrrhh-rol)
- [zambabpm_RRHH.SecurityReportLastGenerated](#zambabpmrrhh-securityreportlastgenerated)
- [zambabpm_RRHH.SLST_S103](#zambabpmrrhh-slsts103)
- [zambabpm_RRHH.SLST_S104](#zambabpmrrhh-slsts104)
- [zambabpm_RRHH.SLST_S105](#zambabpmrrhh-slsts105)
- [zambabpm_RRHH.SLST_S106](#zambabpmrrhh-slsts106)
- [zambabpm_RRHH.SLST_S118](#zambabpmrrhh-slsts118)
- [zambabpm_RRHH.SLST_S119](#zambabpmrrhh-slsts119)
- [zambabpm_RRHH.SLST_S121](#zambabpmrrhh-slsts121)
- [zambabpm_RRHH.SLST_S125](#zambabpmrrhh-slsts125)
- [zambabpm_RRHH.SLST_S126](#zambabpmrrhh-slsts126)
- [zambabpm_RRHH.SLST_S134](#zambabpmrrhh-slsts134)
- [zambabpm_RRHH.SLST_S135](#zambabpmrrhh-slsts135)
- [zambabpm_RRHH.SLST_S137](#zambabpmrrhh-slsts137)
- [zambabpm_RRHH.SLST_S139](#zambabpmrrhh-slsts139)
- [zambabpm_RRHH.SLST_S143](#zambabpmrrhh-slsts143)
- [zambabpm_RRHH.SLST_S144](#zambabpmrrhh-slsts144)
- [zambabpm_RRHH.SLST_S150](#zambabpmrrhh-slsts150)
- [zambabpm_RRHH.SLST_S151](#zambabpmrrhh-slsts151)
- [zambabpm_RRHH.SLST_S152](#zambabpmrrhh-slsts152)
- [zambabpm_RRHH.SLST_S153](#zambabpmrrhh-slsts153)
- [zambabpm_RRHH.SLST_S157](#zambabpmrrhh-slsts157)
- [zambabpm_RRHH.SLST_S159](#zambabpmrrhh-slsts159)
- [zambabpm_RRHH.SLST_S160](#zambabpmrrhh-slsts160)
- [zambabpm_RRHH.SLST_S165](#zambabpmrrhh-slsts165)
- [zambabpm_RRHH.SLST_S178](#zambabpmrrhh-slsts178)
- [zambabpm_RRHH.SLST_S182](#zambabpmrrhh-slsts182)
- [zambabpm_RRHH.SLST_S185](#zambabpmrrhh-slsts185)
- [zambabpm_RRHH.SLST_S186](#zambabpmrrhh-slsts186)
- [zambabpm_RRHH.SLST_S187](#zambabpmrrhh-slsts187)
- [zambabpm_RRHH.SLST_S193](#zambabpmrrhh-slsts193)
- [zambabpm_RRHH.SLST_S194](#zambabpmrrhh-slsts194)
- [zambabpm_RRHH.SLST_S197](#zambabpmrrhh-slsts197)
- [zambabpm_RRHH.SLST_S198](#zambabpmrrhh-slsts198)
- [zambabpm_RRHH.SLST_S199](#zambabpmrrhh-slsts199)
- [zambabpm_RRHH.SLST_S204](#zambabpmrrhh-slsts204)
- [zambabpm_RRHH.SLST_S207](#zambabpmrrhh-slsts207)
- [zambabpm_RRHH.SLST_S216](#zambabpmrrhh-slsts216)
- [zambabpm_RRHH.SLST_S217](#zambabpmrrhh-slsts217)
- [zambabpm_RRHH.SLST_S218](#zambabpmrrhh-slsts218)
- [zambabpm_RRHH.SLST_S219](#zambabpmrrhh-slsts219)
- [zambabpm_RRHH.SLST_S220](#zambabpmrrhh-slsts220)
- [zambabpm_RRHH.SLST_S221](#zambabpmrrhh-slsts221)
- [zambabpm_RRHH.SLST_S223](#zambabpmrrhh-slsts223)
- [zambabpm_RRHH.SLST_S224](#zambabpmrrhh-slsts224)
- [zambabpm_RRHH.SLST_S232](#zambabpmrrhh-slsts232)
- [zambabpm_RRHH.SLST_S233](#zambabpmrrhh-slsts233)
- [zambabpm_RRHH.SLST_S234](#zambabpmrrhh-slsts234)
- [zambabpm_RRHH.SLST_S235](#zambabpmrrhh-slsts235)
- [zambabpm_RRHH.SLST_S238](#zambabpmrrhh-slsts238)
- [zambabpm_RRHH.SLST_S240](#zambabpmrrhh-slsts240)
- [zambabpm_RRHH.SLST_S241](#zambabpmrrhh-slsts241)
- [zambabpm_RRHH.SLST_S269](#zambabpmrrhh-slsts269)
- [zambabpm_RRHH.SLST_S275](#zambabpmrrhh-slsts275)
- [zambabpm_RRHH.SLST_S280](#zambabpmrrhh-slsts280)
- [zambabpm_RRHH.SLST_S285](#zambabpmrrhh-slsts285)
- [zambabpm_RRHH.SLST_S291](#zambabpmrrhh-slsts291)
- [zambabpm_RRHH.SLST_S294](#zambabpmrrhh-slsts294)
- [zambabpm_RRHH.SLST_S296](#zambabpmrrhh-slsts296)
- [zambabpm_RRHH.SLST_S314](#zambabpmrrhh-slsts314)
- [zambabpm_RRHH.SLST_S316](#zambabpmrrhh-slsts316)
- [zambabpm_RRHH.SLST_S317](#zambabpmrrhh-slsts317)
- [zambabpm_RRHH.SLST_S320](#zambabpmrrhh-slsts320)
- [zambabpm_RRHH.SLST_S321](#zambabpmrrhh-slsts321)
- [zambabpm_RRHH.SLST_S330](#zambabpmrrhh-slsts330)
- [zambabpm_RRHH.SLST_S331](#zambabpmrrhh-slsts331)
- [zambabpm_RRHH.SLST_S344](#zambabpmrrhh-slsts344)
- [zambabpm_RRHH.SLST_S346](#zambabpmrrhh-slsts346)
- [zambabpm_RRHH.SLST_S349](#zambabpmrrhh-slsts349)
- [zambabpm_RRHH.SLST_S353](#zambabpmrrhh-slsts353)
- [zambabpm_RRHH.UserEventLogs](#zambabpmrrhh-usereventlogs)
- [zambabpm_RRHH.usr_rightsBKP](#zambabpmrrhh-usrrightsbkp)
- [zambabpm_RRHH.VideoplayerURLSources](#zambabpmrrhh-videoplayerurlsources)
- [zambabpm_RRHH.VideoWidgets](#zambabpmrrhh-videowidgets)
- [zambabpm_RRHH.WidgetsContainer](#zambabpmrrhh-widgetscontainer)
- [zambabpm_RRHH.ZDoSearchResults](#zambabpmrrhh-zdosearchresults)
- [zambabpm_RRHH.ZHierarchy_I118_I119](#zambabpmrrhh-zhierarchyi118i119)
- [zambabpm_RRHH.ZLSR](#zambabpmrrhh-zlsr)
- [zambabpm_RRHH.ZWebBookmarks](#zambabpmrrhh-zwebbookmarks)

## Relaciones entre tablas

Relaciones declaradas mediante claves foraneas en SQL Server. La direccion es **tabla hija.columna -> tabla padre.columna**.

| Tabla hija | Columna hija | Tabla padre | Columna padre | Restriccion |
|------------|--------------|-------------|---------------|-------------|
| dbo.CollaborativeSessions | DocumentId | dbo.CollaborativeDocuments | DocumentId | FK_CollaborativeSessions_CollaborativeDocuments |
| dbo.DOC_T156129 | VOL_ID | dbo.DISK_VOLUME | DISK_VOL_ID | FK__DOC_T1561__VOL_I__7F76C749 |
| dbo.DocumentChanges | DocumentId | dbo.CollaborativeDocuments | DocumentId | FK_DocumentChanges_CollaborativeDocuments |
| dbo.DocumentVersions | DocumentId | dbo.CollaborativeDocuments | DocumentId | FK_DocumentVersions_CollaborativeDocuments |
| zambabpm_RRHH.DOC_B101 | DOC_ID | zambabpm_RRHH.DOC_T101 | DOC_ID | FK_DOCT101_DOCB |
| zambabpm_RRHH.DOC_B102 | DOC_ID | zambabpm_RRHH.DOC_T102 | DOC_ID | FK_DOCT102_DOCB |
| zambabpm_RRHH.DOC_B103 | DOC_ID | zambabpm_RRHH.DOC_T103 | DOC_ID | FK_DOCT103_DOCB |
| zambabpm_RRHH.DOC_B104 | DOC_ID | zambabpm_RRHH.DOC_T104 | DOC_ID | FK_DOCT104_DOCB |
| zambabpm_RRHH.DOC_B105 | DOC_ID | zambabpm_RRHH.DOC_T105 | DOC_ID | FK_DOCT105_DOCB |
| zambabpm_RRHH.DOC_B107 | DOC_ID | zambabpm_RRHH.DOC_T107 | DOC_ID | FK_DOCT107_DOCB |
| zambabpm_RRHH.DOC_B108 | DOC_ID | zambabpm_RRHH.DOC_T108 | DOC_ID | FK_DOCT108_DOCB |
| zambabpm_RRHH.DOC_B109 | DOC_ID | zambabpm_RRHH.DOC_T109 | DOC_ID | FK_DOCT109_DOCB |
| zambabpm_RRHH.DOC_B110 | DOC_ID | zambabpm_RRHH.DOC_T110 | DOC_ID | FK_DOCT110_DOCB |
| zambabpm_RRHH.DOC_B111 | DOC_ID | zambabpm_RRHH.DOC_T111 | DOC_ID | FK_DOCT111_DOCB |
| zambabpm_RRHH.DOC_B113 | DOC_ID | zambabpm_RRHH.DOC_T113 | DOC_ID | FK_DOCT113_DOCB |
| zambabpm_RRHH.DOC_B114 | DOC_ID | zambabpm_RRHH.DOC_T114 | DOC_ID | FK_DOCT114_DOCB |
| zambabpm_RRHH.DOC_B118 | DOC_ID | zambabpm_RRHH.DOC_T118 | DOC_ID | FK_DOCT118_DOCB |
| zambabpm_RRHH.DOC_B119 | DOC_ID | zambabpm_RRHH.DOC_T119 | DOC_ID | FK_DOCT119_DOCB |
| zambabpm_RRHH.DOC_B120 | DOC_ID | zambabpm_RRHH.DOC_T120 | DOC_ID | FK_DOCT120_DOCB |
| zambabpm_RRHH.DOC_B121 | DOC_ID | zambabpm_RRHH.DOC_T121 | DOC_ID | FK_DOCT121_DOCB |
| zambabpm_RRHH.DOC_B122 | DOC_ID | zambabpm_RRHH.DOC_T122 | DOC_ID | FK_DOCT122_DOCB |
| zambabpm_RRHH.DOC_B123 | DOC_ID | zambabpm_RRHH.DOC_T123 | DOC_ID | FK_DOCT123_DOCB |
| zambabpm_RRHH.DOC_B124 | DOC_ID | zambabpm_RRHH.DOC_T124 | DOC_ID | FK_DOCT124_DOCB |
| zambabpm_RRHH.DOC_B125 | DOC_ID | zambabpm_RRHH.DOC_T125 | DOC_ID | FK_DOCT125_DOCB |
| zambabpm_RRHH.DOC_B126 | DOC_ID | zambabpm_RRHH.DOC_T126 | DOC_ID | FK_DOCT126_DOCB |
| zambabpm_RRHH.DOC_B129 | DOC_ID | zambabpm_RRHH.DOC_T129 | DOC_ID | FK_DOCT129_DOCB |
| zambabpm_RRHH.DOC_B130 | DOC_ID | zambabpm_RRHH.DOC_T130 | DOC_ID | FK_DOCT130_DOCB |
| zambabpm_RRHH.DOC_B131 | DOC_ID | zambabpm_RRHH.DOC_T131 | DOC_ID | FK_DOCT131_DOCB |
| zambabpm_RRHH.DOC_B132 | DOC_ID | zambabpm_RRHH.DOC_T132 | DOC_ID | FK_DOCT132_DOCB |
| zambabpm_RRHH.DOC_B134 | DOC_ID | zambabpm_RRHH.DOC_T134 | DOC_ID | FK_DOCT134_DOCB |
| zambabpm_RRHH.DOC_B135 | DOC_ID | zambabpm_RRHH.DOC_T135 | DOC_ID | FK_DOCT135_DOCB |
| zambabpm_RRHH.DOC_B136 | DOC_ID | zambabpm_RRHH.DOC_T136 | DOC_ID | FK_DOCT136_DOCB |
| zambabpm_RRHH.DOC_B137 | DOC_ID | zambabpm_RRHH.DOC_T137 | DOC_ID | FK_DOCT137_DOCB |
| zambabpm_RRHH.DOC_B138 | DOC_ID | zambabpm_RRHH.DOC_T138 | DOC_ID | FK_DOCT138_DOCB |
| zambabpm_RRHH.DOC_B139 | DOC_ID | zambabpm_RRHH.DOC_T139 | DOC_ID | FK_DOCT139_DOCB |
| zambabpm_RRHH.DOC_B140 | DOC_ID | zambabpm_RRHH.DOC_T140 | DOC_ID | FK_DOCT140_DOCB |
| zambabpm_RRHH.DOC_B142 | DOC_ID | zambabpm_RRHH.DOC_T142 | DOC_ID | FK_DOCT142_DOCB |
| zambabpm_RRHH.DOC_B143 | DOC_ID | zambabpm_RRHH.DOC_T143 | DOC_ID | FK_DOCT143_DOCB |
| zambabpm_RRHH.DOC_B144 | DOC_ID | zambabpm_RRHH.DOC_T144 | DOC_ID | FK_DOCT144_DOCB |
| zambabpm_RRHH.DOC_B145 | DOC_ID | zambabpm_RRHH.DOC_T145 | DOC_ID | FK_DOCT145_DOCB |
| zambabpm_RRHH.DOC_B146 | DOC_ID | zambabpm_RRHH.DOC_T146 | DOC_ID | FK_DOCT146_DOCB |
| zambabpm_RRHH.DOC_B148 | DOC_ID | zambabpm_RRHH.DOC_T148 | DOC_ID | FK_DOCT148_DOCB |
| zambabpm_RRHH.DOC_B149 | DOC_ID | zambabpm_RRHH.DOC_T149 | DOC_ID | FK_DOCT149_DOCB |
| zambabpm_RRHH.DOC_B150 | DOC_ID | zambabpm_RRHH.DOC_T150 | DOC_ID | FK_DOCT150_DOCB |
| zambabpm_RRHH.DOC_B151 | DOC_ID | zambabpm_RRHH.DOC_T151 | DOC_ID | FK_DOCT151_DOCB |
| zambabpm_RRHH.DOC_B152 | DOC_ID | zambabpm_RRHH.DOC_T152 | DOC_ID | FK_DOCT152_DOCB |
| zambabpm_RRHH.DOC_I101 | DOC_ID | zambabpm_RRHH.DOC_T101 | DOC_ID | FK__DOC_I101__DOC_ID__15BB0E23 |
| zambabpm_RRHH.DOC_I101 | I234 | zambabpm_RRHH.SLST_S234 | Codigo | FK_DOC_T101_SLST_S234 |
| zambabpm_RRHH.DOC_I102 | DOC_ID | zambabpm_RRHH.DOC_T102 | DOC_ID | FK__DOC_I102__DOC_ID__23150941 |
| zambabpm_RRHH.DOC_I103 | DOC_ID | zambabpm_RRHH.DOC_T103 | DOC_ID | FK__DOC_I103__DOC_ID__306F045F |
| zambabpm_RRHH.DOC_I103 | I103 | zambabpm_RRHH.SLST_S103 | Codigo | FK_DOC_T103_SLST_S103 |
| zambabpm_RRHH.DOC_I103 | I104 | zambabpm_RRHH.SLST_S104 | Codigo | FK_DOC_T103_SLST_S104 |
| zambabpm_RRHH.DOC_I103 | I106 | zambabpm_RRHH.SLST_S106 | Codigo | FK_DOC_T103_SLST_S106 |
| zambabpm_RRHH.DOC_I103 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T103_SLST_S118 |
| zambabpm_RRHH.DOC_I103 | I218 | zambabpm_RRHH.SLST_S218 | Codigo | FK_DOC_T103_SLST_S218 |
| zambabpm_RRHH.DOC_I104 | DOC_ID | zambabpm_RRHH.DOC_T104 | DOC_ID | FK__DOC_I104__DOC_ID__3DC8FF7D |
| zambabpm_RRHH.DOC_I105 | DOC_ID | zambabpm_RRHH.DOC_T105 | DOC_ID | FK__DOC_I105__DOC_ID__52C41C63 |
| zambabpm_RRHH.DOC_I105 | I194 | zambabpm_RRHH.SLST_S194 | Codigo | FK_DOC_T105_SLST_S194 |
| zambabpm_RRHH.DOC_I107 | DOC_ID | zambabpm_RRHH.DOC_T107 | DOC_ID | FK__DOC_I107__DOC_ID__7148A383 |
| zambabpm_RRHH.DOC_I107 | I160 | zambabpm_RRHH.SLST_S160 | Codigo | FK_DOC_T107_SLST_S160 |
| zambabpm_RRHH.DOC_I107 | I240 | zambabpm_RRHH.SLST_S240 | Codigo | FK_DOC_T107_SLST_S240 |
| zambabpm_RRHH.DOC_I108 | DOC_ID | zambabpm_RRHH.DOC_T108 | DOC_ID | FK__DOC_I108__DOC_ID__008AE713 |
| zambabpm_RRHH.DOC_I109 | DOC_ID | zambabpm_RRHH.DOC_T109 | DOC_ID | FK__DOC_I109__DOC_ID__5224FDE5 |
| zambabpm_RRHH.DOC_I110 | DOC_ID | zambabpm_RRHH.DOC_T110 | DOC_ID | FK__DOC_I110__DOC_ID__7C1B37B1 |
| zambabpm_RRHH.DOC_I111 | DOC_ID | zambabpm_RRHH.DOC_T111 | DOC_ID | FK__DOC_I111__DOC_ID__28EDDE28 |
| zambabpm_RRHH.DOC_I111 | I294 | zambabpm_RRHH.SLST_S294 | Codigo | FK_DOC_T111_SLST_S294 |
| zambabpm_RRHH.DOC_I113 | DOC_ID | zambabpm_RRHH.DOC_T113 | DOC_ID | FK__DOC_I113__DOC_ID__640EA3F6 |
| zambabpm_RRHH.DOC_I113 | I121 | zambabpm_RRHH.SLST_S121 | Codigo | FK_DOC_T113_SLST_S121 |
| zambabpm_RRHH.DOC_I113 | I152 | zambabpm_RRHH.SLST_S152 | Codigo | FK_DOC_T113_SLST_S152 |
| zambabpm_RRHH.DOC_I113 | I153 | zambabpm_RRHH.SLST_S153 | Codigo | FK_DOC_T113_SLST_S153 |
| zambabpm_RRHH.DOC_I113 | I204 | zambabpm_RRHH.SLST_S204 | Codigo | FK_DOC_T113_SLST_S204 |
| zambabpm_RRHH.DOC_I113 | I235 | zambabpm_RRHH.SLST_S235 | Codigo | FK_DOC_T113_SLST_S235 |
| zambabpm_RRHH.DOC_I114 | DOC_ID | zambabpm_RRHH.DOC_T114 | DOC_ID | FK__DOC_I114__DOC_ID__2E71AD54 |
| zambabpm_RRHH.DOC_I118 | DOC_ID | zambabpm_RRHH.DOC_T118 | DOC_ID | FK__DOC_I118__DOC_ID__67AA2AB0 |
| zambabpm_RRHH.DOC_I118 | I103 | zambabpm_RRHH.SLST_S103 | Codigo | FK_DOC_T118_SLST_S103 |
| zambabpm_RRHH.DOC_I118 | I104 | zambabpm_RRHH.SLST_S104 | Codigo | FK_DOC_T118_SLST_S104 |
| zambabpm_RRHH.DOC_I118 | I106 | zambabpm_RRHH.SLST_S106 | Codigo | FK_DOC_T118_SLST_S106 |
| zambabpm_RRHH.DOC_I118 | I144 | zambabpm_RRHH.SLST_S144 | Codigo | FK_DOC_T118_SLST_S144 |
| zambabpm_RRHH.DOC_I118 | I153 | zambabpm_RRHH.SLST_S153 | Codigo | FK_DOC_T118_SLST_S153 |
| zambabpm_RRHH.DOC_I118 | I193 | zambabpm_RRHH.SLST_S193 | Codigo | FK_DOC_T118_SLST_S193 |
| zambabpm_RRHH.DOC_I118 | I194 | zambabpm_RRHH.SLST_S194 | Codigo | FK_DOC_T118_SLST_S194 |
| zambabpm_RRHH.DOC_I118 | I198 | zambabpm_RRHH.SLST_S198 | Codigo | FK_DOC_T118_SLST_S198 |
| zambabpm_RRHH.DOC_I118 | I199 | zambabpm_RRHH.SLST_S199 | Codigo | FK_DOC_T118_SLST_S199 |
| zambabpm_RRHH.DOC_I118 | I240 | zambabpm_RRHH.SLST_S240 | Codigo | FK_DOC_T118_SLST_S240 |
| zambabpm_RRHH.DOC_I118 | I241 | zambabpm_RRHH.SLST_S241 | Codigo | FK_DOC_T118_SLST_S241 |
| zambabpm_RRHH.DOC_I119 | DOC_ID | zambabpm_RRHH.DOC_T119 | DOC_ID | FK__DOC_I119__DOC_ID__750425CE |
| zambabpm_RRHH.DOC_I119 | I106 | zambabpm_RRHH.SLST_S106 | Codigo | FK_DOC_T119_SLST_S106 |
| zambabpm_RRHH.DOC_I120 | DOC_ID | zambabpm_RRHH.DOC_T120 | DOC_ID | FK__DOC_I120__DOC_ID__3C55B857 |
| zambabpm_RRHH.DOC_I121 | DOC_ID | zambabpm_RRHH.DOC_T121 | DOC_ID | FK__DOC_I121__DOC_ID__344A823B |
| zambabpm_RRHH.DOC_I121 | I216 | zambabpm_RRHH.SLST_S216 | Codigo | FK_DOC_T121_SLST_S216 |
| zambabpm_RRHH.DOC_I121 | I218 | zambabpm_RRHH.SLST_S218 | Codigo | FK_DOC_T121_SLST_S218 |
| zambabpm_RRHH.DOC_I122 | DOC_ID | zambabpm_RRHH.DOC_T122 | DOC_ID | FK__DOC_I122__DOC_ID__35FDC083 |
| zambabpm_RRHH.DOC_I122 | I152 | zambabpm_RRHH.SLST_S152 | Codigo | FK_DOC_T122_SLST_S152 |
| zambabpm_RRHH.DOC_I122 | I235 | zambabpm_RRHH.SLST_S235 | Codigo | FK_DOC_T122_SLST_S235 |
| zambabpm_RRHH.DOC_I122 | I238 | zambabpm_RRHH.SLST_S238 | Codigo | FK_DOC_T122_SLST_S238 |
| zambabpm_RRHH.DOC_I123 | DOC_ID | zambabpm_RRHH.DOC_T123 | DOC_ID | FK__DOC_I123__DOC_ID__75E33B6E |
| zambabpm_RRHH.DOC_I124 | DOC_ID | zambabpm_RRHH.DOC_T124 | DOC_ID | FK__DOC_I124__DOC_ID__033D368C |
| zambabpm_RRHH.DOC_I124 | I152 | zambabpm_RRHH.SLST_S152 | Codigo | FK_DOC_T124_SLST_S152 |
| zambabpm_RRHH.DOC_I124 | I235 | zambabpm_RRHH.SLST_S235 | Codigo | FK_DOC_T124_SLST_S235 |
| zambabpm_RRHH.DOC_I125 | DOC_ID | zambabpm_RRHH.DOC_T125 | DOC_ID | FK__DOC_I125__DOC_ID__62F0628B |
| zambabpm_RRHH.DOC_I125 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T125_SLST_S118 |
| zambabpm_RRHH.DOC_I125 | I160 | zambabpm_RRHH.SLST_S160 | Codigo | FK_DOC_T125_SLST_S160 |
| zambabpm_RRHH.DOC_I125 | I224 | zambabpm_RRHH.SLST_S224 | Codigo | FK_DOC_T125_SLST_S224 |
| zambabpm_RRHH.DOC_I125 | I285 | zambabpm_RRHH.SLST_S285 | Codigo | FK_DOC_T125_SLST_S285 |
| zambabpm_RRHH.DOC_I126 | DOC_ID | zambabpm_RRHH.DOC_T126 | DOC_ID | FK__DOC_I126__DOC_ID__0080C572 |
| zambabpm_RRHH.DOC_I126 | I185 | zambabpm_RRHH.SLST_S185 | Codigo | FK_DOC_T126_SLST_S185 |
| zambabpm_RRHH.DOC_I126 | I186 | zambabpm_RRHH.SLST_S186 | Codigo | FK_DOC_T126_SLST_S186 |
| zambabpm_RRHH.DOC_I126 | I233 | zambabpm_RRHH.SLST_S233 | Codigo | FK_DOC_T126_SLST_S233 |
| zambabpm_RRHH.DOC_I129 | DOC_ID | zambabpm_RRHH.DOC_T129 | DOC_ID | FK__DOC_I129__DOC_ID__5CCD74E1 |
| zambabpm_RRHH.DOC_I130 | DOC_ID | zambabpm_RRHH.DOC_T130 | DOC_ID | FK__DOC_I130__DOC_ID__254835CD |
| zambabpm_RRHH.DOC_I130 | I275 | zambabpm_RRHH.SLST_S275 | Codigo | FK_DOC_T130_SLST_S275 |
| zambabpm_RRHH.DOC_I131 | DOC_ID | zambabpm_RRHH.DOC_T131 | DOC_ID | FK__DOC_I131__DOC_ID__3A4352B3 |
| zambabpm_RRHH.DOC_I131 | I269 | zambabpm_RRHH.SLST_S269 | Codigo | FK_DOC_T131_SLST_S269 |
| zambabpm_RRHH.DOC_I132 | DOC_ID | zambabpm_RRHH.DOC_T132 | DOC_ID | FK__DOC_I132__DOC_ID__2236BEF8 |
| zambabpm_RRHH.DOC_I132 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T132_SLST_S118 |
| zambabpm_RRHH.DOC_I132 | I151 | zambabpm_RRHH.SLST_S151 | Codigo | FK_DOC_T132_SLST_S151 |
| zambabpm_RRHH.DOC_I132 | I160 | zambabpm_RRHH.SLST_S160 | Codigo | FK_DOC_T132_SLST_S160 |
| zambabpm_RRHH.DOC_I132 | I219 | zambabpm_RRHH.SLST_S219 | Codigo | FK_DOC_T132_SLST_S219 |
| zambabpm_RRHH.DOC_I132 | I224 | zambabpm_RRHH.SLST_S224 | Codigo | FK_DOC_T132_SLST_S224 |
| zambabpm_RRHH.DOC_I132 | I280 | zambabpm_RRHH.SLST_S280 | Codigo | FK_DOC_T132_SLST_S280 |
| zambabpm_RRHH.DOC_I132 | I294 | zambabpm_RRHH.SLST_S294 | Codigo | FK_DOC_T132_SLST_S294 |
| zambabpm_RRHH.DOC_I134 | DOC_ID | zambabpm_RRHH.DOC_T134 | DOC_ID | FK__DOC_I134__DOC_ID__080CD8A1 |
| zambabpm_RRHH.DOC_I134 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T134_SLST_S118 |
| zambabpm_RRHH.DOC_I134 | I160 | zambabpm_RRHH.SLST_S160 | Codigo | FK_DOC_T134_SLST_S160 |
| zambabpm_RRHH.DOC_I134 | I224 | zambabpm_RRHH.SLST_S224 | Codigo | FK_DOC_T134_SLST_S224 |
| zambabpm_RRHH.DOC_I134 | I285 | zambabpm_RRHH.SLST_S285 | Codigo | FK_DOC_T134_SLST_S285 |
| zambabpm_RRHH.DOC_I135 | DOC_ID | zambabpm_RRHH.DOC_T135 | DOC_ID | FK__DOC_I135__DOC_ID__49DA9BFE |
| zambabpm_RRHH.DOC_I135 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T135_SLST_S118 |
| zambabpm_RRHH.DOC_I135 | I151 | zambabpm_RRHH.SLST_S151 | Codigo | FK_DOC_T135_SLST_S151 |
| zambabpm_RRHH.DOC_I135 | I160 | zambabpm_RRHH.SLST_S160 | Codigo | FK_DOC_T135_SLST_S160 |
| zambabpm_RRHH.DOC_I135 | I219 | zambabpm_RRHH.SLST_S219 | Codigo | FK_DOC_T135_SLST_S219 |
| zambabpm_RRHH.DOC_I135 | I224 | zambabpm_RRHH.SLST_S224 | Codigo | FK_DOC_T135_SLST_S224 |
| zambabpm_RRHH.DOC_I135 | I280 | zambabpm_RRHH.SLST_S280 | Codigo | FK_DOC_T135_SLST_S280 |
| zambabpm_RRHH.DOC_I136 | DOC_ID | zambabpm_RRHH.DOC_T136 | DOC_ID | FK__DOC_I136__DOC_ID__69DD573C |
| zambabpm_RRHH.DOC_I137 | DOC_ID | zambabpm_RRHH.DOC_T137 | DOC_ID | FK__DOC_I137__DOC_ID__0E1AB7B2 |
| zambabpm_RRHH.DOC_I138 | DOC_ID | zambabpm_RRHH.DOC_T138 | DOC_ID | FK__DOC_I138__DOC_ID__11813442 |
| zambabpm_RRHH.DOC_I139 | DOC_ID | zambabpm_RRHH.DOC_T139 | DOC_ID | FK__DOC_I139__DOC_ID__656DA7DA |
| zambabpm_RRHH.DOC_I139 | I204 | zambabpm_RRHH.SLST_S204 | Codigo | FK_DOC_T139_SLST_S204 |
| zambabpm_RRHH.DOC_I139 | I317 | zambabpm_RRHH.SLST_S317 | Codigo | FK_DOC_T139_SLST_S317 |
| zambabpm_RRHH.DOC_I140 | DOC_ID | zambabpm_RRHH.DOC_T140 | DOC_ID | FK__DOC_I140__DOC_ID__142896C3 |
| zambabpm_RRHH.DOC_I140 | I320 | zambabpm_RRHH.SLST_S320 | Codigo | FK_DOC_T140_SLST_S320 |
| zambabpm_RRHH.DOC_I142 | DOC_ID | zambabpm_RRHH.DOC_T142 | DOC_ID | FK__DOC_I142__DOC_ID__43D7A9E5 |
| zambabpm_RRHH.DOC_I142 | I118 | zambabpm_RRHH.SLST_S118 | Codigo | FK_DOC_T142_SLST_S118 |
| zambabpm_RRHH.DOC_I142 | I119 | zambabpm_RRHH.SLST_S119 | Codigo | FK_DOC_T142_SLST_S119 |
| zambabpm_RRHH.DOC_I142 | I121 | zambabpm_RRHH.SLST_S121 | Codigo | FK_DOC_T142_SLST_S121 |
| zambabpm_RRHH.DOC_I143 | DOC_ID | zambabpm_RRHH.DOC_T143 | DOC_ID | FK__DOC_I143__DOC_ID__7386BD07 |
| zambabpm_RRHH.DOC_I143 | I320 | zambabpm_RRHH.SLST_S320 | Codigo | FK_DOC_T143_SLST_S320 |
| zambabpm_RRHH.DOC_I144 | DOC_ID | zambabpm_RRHH.DOC_T144 | DOC_ID | FK__DOC_I144__DOC_ID__5D6271BE |
| zambabpm_RRHH.DOC_I145 | DOC_ID | zambabpm_RRHH.DOC_T145 | DOC_ID | FK__DOC_I145__DOC_ID__69C848A3 |
| zambabpm_RRHH.DOC_I146 | DOC_ID | zambabpm_RRHH.DOC_T146 | DOC_ID | FK__DOC_I146__DOC_ID__762E1F88 |
| zambabpm_RRHH.DOC_I146 | I353 | zambabpm_RRHH.SLST_S353 | Codigo | FK_DOC_T146_SLST_S353 |
| zambabpm_RRHH.DOC_I148 | DOC_ID | zambabpm_RRHH.DOC_T148 | DOC_ID | FK__DOC_I148__DOC_ID__2AA1E7C7 |
| zambabpm_RRHH.DOC_I148 | I330 | zambabpm_RRHH.SLST_S330 | Codigo | FK_DOC_T148_SLST_S330 |
| zambabpm_RRHH.DOC_I148 | I331 | zambabpm_RRHH.SLST_S331 | Codigo | FK_DOC_T148_SLST_S331 |
| zambabpm_RRHH.DOC_I149 | DOC_ID | zambabpm_RRHH.DOC_T149 | DOC_ID | FK__DOC_I149__DOC_ID__016AC80A |
| zambabpm_RRHH.DOC_I149 | I330 | zambabpm_RRHH.SLST_S330 | Codigo | FK_DOC_T149_SLST_S330 |
| zambabpm_RRHH.DOC_I149 | I331 | zambabpm_RRHH.SLST_S331 | Codigo | FK_DOC_T149_SLST_S331 |
| zambabpm_RRHH.DOC_I150 | DOC_ID | zambabpm_RRHH.DOC_T150 | DOC_ID | FK__DOC_I150__DOC_ID__2E3D6E81 |
| zambabpm_RRHH.DOC_I151 | DOC_ID | zambabpm_RRHH.DOC_T151 | DOC_ID | FK__DOC_I151__DOC_ID__0D9B94C5 |
| zambabpm_RRHH.DOC_I151 | I344 | zambabpm_RRHH.SLST_S344 | Codigo | FK_DOC_T151_SLST_S344 |
| zambabpm_RRHH.DOC_I152 | DOC_ID | zambabpm_RRHH.DOC_T152 | DOC_ID | FK__DOC_I152__DOC_ID__2FF0ACC9 |
| zambabpm_RRHH.DOC_I152 | I330 | zambabpm_RRHH.SLST_S330 | Codigo | FK_DOC_T152_SLST_S330 |
| zambabpm_RRHH.DOC_T138 | VOL_ID | dbo.DISK_VOLUME | DISK_VOL_ID | FK__DOC_T138__VOL_ID__0CBC7F25 |
| zambabpm_RRHH.DOC_T138 | VOL_ID | dbo.DISK_VOLUME | DISK_VOL_ID | FK_DOC_T138_DISK_VOLUME |
| zambabpm_RRHH.ZHierarchy_I118_I119 | ParentValue | zambabpm_RRHH.SLST_S118 | Codigo | HierarchicalFK_772797934 |
| zambabpm_RRHH.ZHierarchy_I118_I119 | ChildValue | zambabpm_RRHH.SLST_S119 | Codigo | HierarchicalFK_971441277 |

---

## dbo.980s

**Row count:** 2066

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Sin | numeric(18,0) | YES |  | |
| Op | numeric(18,0) | YES |  | |
| ZAMI | numeric(18,0) | YES |  | |
| Poliza | numeric(18,0) | YES |  | |

---

## dbo.Action_Param

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Name | varchar(20) | NO | PK | |
| Type | varchar(20) | YES |  | |
| Value | varchar(20) | YES |  | |
| Action_Id | int | YES |  | |
| Query_Order | int | YES |  | |
| Param_Id | int | YES |  | |

---

## dbo.Asignar

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Agencia | nvarchar(255) | YES |  | |
| Usuario | nvarchar(255) | YES |  | |

---

## dbo.Automail

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | char(10) | NO | PK | |
| Name | varchar(100) | NO |  | |
| CC | varchar(200) | YES |  | |
| CCO | varchar(200) | YES |  | |
| MailTo | varchar(200) | YES |  | |
| MailFrom | varchar(50) | NO |  | |
| Subject | varchar(150) | YES |  | |
| Body | char(2000) | YES |  | |
| Confirmation | bit | YES |  | |
| PathImages | varchar(4000) | YES |  | |
| PathFiles | varchar(4000) | YES |  | |

---

## dbo.CaracterxNroDoForEach

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Nro | varchar(50) | NO |  | |
| CantCaracteres | varchar(100) | NO |  | |

---

## dbo.ChatHistories

**Row count:** 70

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | decimal(18,0) | NO | PK | |
| ChatId | decimal(18,0) | NO |  | |
| UserId | decimal(18,0) | NO |  | |
| Message | nvarchar(-1) | YES |  | |
| Date | datetime | NO |  | |

---

## dbo.ChatPeoples

**Row count:** 26

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | decimal(18,0) | NO | PK | |
| ChatId | decimal(18,0) | NO |  | |
| UserId | decimal(18,0) | NO |  | |

---

## dbo.Chats

**Row count:** 13

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | decimal(18,0) | NO | PK | |
| AdminId | decimal(18,0) | NO |  | |
| LastMessage | datetime | NO |  | |
| ChatType | decimal(18,0) | NO |  | |
| ChatName | nvarchar(-1) | YES |  | |
| DocId | int | YES |  | |

---

## dbo.ChatUsers

**Row count:** 17

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | decimal(18,0) | NO | PK | |
| Name | nvarchar(-1) | YES |  | |
| Avatar | nvarchar(-1) | YES |  | |
| Status | decimal(18,0) | NO |  | |
| LastActiveOn | datetime | NO |  | |
| RoomId | nvarchar(-1) | YES |  | |
| Role | decimal(18,0) | YES |  | |
| Email | nvarchar(-1) | YES |  | |
| Company | nvarchar(-1) | YES |  | |
| Password | nvarchar(-1) | YES |  | |
| Position | varchar(-1) | YES |  | |
| InternalPhone | varchar(-1) | YES |  | |
| Phone | varchar(-1) | YES |  | |
| CellPhone | varchar(-1) | YES |  | |

---

## dbo.CheckListEmision

**Row count:** 258

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| nuseccion | smallint | NO |  | |
| cotipomovimiento | smallint | NO |  | |
| deconcepto | varchar(50) | NO |  | |
| coobligatorio | char(1) | NO |  | |
| Index_Id | numeric(18,0) | YES |  | |

---

## dbo.checklistreclamo

**Row count:** 146

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| HechoGenerador | numeric(18,0) | NO |  | |
| deconcepto | nvarchar(255) | NO |  | |
| coobligatorio | nvarchar(1) | NO |  | |
| Index_Id | numeric(18,0) | NO |  | |

---

## dbo.CollaborativeDocuments

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocumentId | nvarchar(128) | NO | PK | |
| EntityId | nvarchar(128) | YES |  | |
| UserId | nvarchar(128) | YES |  | |
| DocumentName | nvarchar(256) | YES |  | |
| Content | nvarchar(-1) | YES |  | |
| MimeType | nvarchar(128) | YES |  | |
| Version | int | NO |  | |
| CreatedAt | datetime2 | NO |  | |
| LastModifiedAt | datetime2 | NO |  | |
| IsDeleted | bit | NO |  | |

---

## dbo.CollaborativeSessions

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DocumentId -> dbo.CollaborativeDocuments.DocumentId ($(@{parent_schema=dbo; parent_table=CollaborativeSessions; parent_column=DocumentId; ref_schema=dbo; ref_table=CollaborativeDocuments; ref_column=DocumentId; constraint_name=FK_CollaborativeSessions_CollaborativeDocuments}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| SessionId | nvarchar(128) | NO | PK | |
| DocumentId | nvarchar(128) | NO |  | |
| UserId | nvarchar(128) | NO |  | |
| ClientId | nvarchar(128) | NO |  | |
| UserEmail | nvarchar(256) | YES |  | |
| UserDisplayName | nvarchar(256) | YES |  | |
| CursorPosition | int | NO |  | |
| SelectionStart | int | NO |  | |
| SelectionEnd | int | NO |  | |
| CursorColor | nvarchar(10) | YES |  | |
| ClientVersion | int | NO |  | |
| AcknowledgedVersion | int | NO |  | |
| IsActive | bit | NO |  | |
| CreatedAt | datetime2 | NO |  | |
| LastHeartbeat | datetime2 | NO |  | |

---

## dbo.ContadoresWf

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Item | nvarchar(20) | NO |  | |
| Value | nvarchar(20) | NO |  | |

---

## dbo.DatosStores

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IDStore | numeric(18,0) | YES |  | |
| NombreStore | nvarchar(50) | YES |  | |
| Servidor | nvarchar(50) | YES |  | |
| Base de Datos | nvarchar(50) | YES |  | |
| Usuario | nvarchar(50) | YES |  | |
| Password | nvarchar(50) | YES |  | |
| Descripcion | nvarchar(50) | YES |  | |

---

## dbo.Default_Search

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocTypeId | numeric(18,0) | NO | PK | |
| Value | varchar(300) | YES |  | |
| IndexId | numeric(18,0) | NO | PK | |
| UserId | numeric(18,0) | NO | PK | |
| Comparate | char(50) | YES |  | |

---

## dbo.DISK_GROUP

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DISK_GROUP_ID | numeric(18,0) | NO | PK | |
| DISK_GROUP_NAME | nvarchar(30) | NO |  | |
| DISK_GROUP_CRDATE | datetime | YES |  | |

---

## dbo.DISK_GROUP_R_DISK_VOLUME

**Row count:** 33

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DISK_GROUP_ID | numeric(18,0) | NO | PK | |
| DISK_VOLUME_ID | int | NO | PK | |

---

## dbo.DISK_VOLUME

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DISK_VOL_ID | int | NO | PK | |
| DISK_VOL_NAME | nvarchar(50) | NO |  | |
| DISK_VOL_SIZE | decimal(18,0) | YES |  | |
| DISK_VOL_TYPE | int | YES |  | |
| DISK_VOL_COPY | int | NO |  | |
| DISK_VOL_PATH | nvarchar(255) | NO |  | |
| DISK_VOL_SIZE_LEN | decimal(18,0) | YES |  | |
| DISK_VOL_STATE | int | YES |  | |
| DISK_VOL_LSTOFFSET | int | YES |  | |
| DISK_VOL_FILES | decimal(18,0) | NO |  | |

---

## dbo.DOC_INDEX

**Row count:** 233

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| INDEX_ID | numeric(18,0) | NO | PK | |
| INDEX_NAME | char(100) | NO |  | |
| INDEX_TYPE | char(10) | YES |  | |
| INDEX_LEN | numeric(10,0) | YES |  | |
| AUTOFILL | numeric(18,0) | YES |  | |
| NOINDEX | numeric(18,0) | YES |  | |
| DROPDOWN | numeric(10,0) | YES |  | |
| AUTODISPLAY | numeric(18,0) | YES |  | |
| INVISIBLE | numeric(18,0) | YES |  | |
| OBJECT_TYPE_ID | numeric(10,0) | YES |  | |
| DROPDOWNTYPE | numeric(18,0) | YES |  | |
| ALLOWDATAOUTOFLIST | numeric(18,0) | YES |  | |

---

## dbo.DOC_NOTES

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| NOTE_ID | numeric(18,0) | NO | PK | |
| X_POSITION | numeric(10,0) | NO |  | |
| Y_POSITION | numeric(10,0) | NO |  | |
| X_SIZE | numeric(10,0) | NO |  | |
| Y_SIZE | numeric(10,0) | NO |  | |
| USER_ID | numeric(18,0) | NO |  | |
| NOTE_DATE | nvarchar(50) | NO |  | |
| NOTE_TIME | nvarchar(50) | NO |  | |
| NOTE_TEXT | nvarchar(255) | YES |  | |
| DOC_ID | nvarchar(10) | NO |  | |
| DOC_TYPE_ID | numeric(18,0) | YES |  | |
| USER_NAME | nvarchar(50) | NO |  | |
| Type | numeric(10,0) | YES |  | |
| ImageId | numeric(18,0) | YES |  | |
| Encrypted | numeric(10,0) | YES |  | |

---

## dbo.DOC_RESTRICTION_R_USER

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(10,0) | NO | PK | |
| RESTRICTION_ID | numeric(18,0) | NO | PK | |

---

## dbo.DOC_RESTRICTIONS

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_ID | numeric(18,0) | NO |  | |
| INDEX_ID | numeric(18,0) | NO |  | |
| STRING_VALUE | nvarchar(3000) | YES |  | |
| RESTRICTION_ID | numeric(18,0) | NO | PK | |
| RESTRICTION_NAME | nvarchar(500) | YES |  | |

---

## dbo.DOC_T156129

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- VOL_ID -> dbo.DISK_VOLUME.DISK_VOL_ID ($(@{parent_schema=dbo; parent_table=DOC_T156129; parent_column=VOL_ID; ref_schema=dbo; ref_table=DISK_VOLUME; ref_column=DISK_VOL_ID; constraint_name=FK__DOC_T1561__VOL_I__7F76C749}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## dbo.DOC_TYPE

**Row count:** 42

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_ID | numeric(18,0) | NO | PK | |
| DOC_TYPE_NAME | char(60) | YES |  | |
| FILE_FORMAT_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | numeric(18,0) | YES |  | |
| THUMBNAILS | numeric(18,0) | YES |  | |
| ICON_ID | numeric(18,0) | YES |  | |
| CROSS_REFERENCE | bit | YES |  | |
| LIFE_CYCLE | bit | YES |  | |
| OBJECT_TYPE_ID | numeric(4,0) | YES |  | |
| AUTONAME | nvarchar(255) | YES |  | |
| DOCCOUNT | numeric(18,0) | YES |  | |
| DocumentalId | numeric(18,0) | YES |  | |
| MB | decimal(18,0) | YES |  | |

---

## dbo.DOC_TYPE_GROUP

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_GROUP_NAME | nvarchar(30) | NO |  | |
| ICON | numeric(4,0) | NO |  | |
| PARENT_ID | numeric(10,0) | NO |  | |
| OBJECT_TYPE_ID | numeric(4,0) | NO |  | |
| DOC_TYPE_GROUP_ID | numeric(18,0) | NO |  | |

---

## dbo.DOC_TYPE_R_DOC_TYPE

**Row count:** 159

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocTypeID1 | numeric(18,0) | NO | PK | |
| DocTypeID2 | numeric(18,0) | NO | PK | |
| Index1 | numeric(18,0) | NO | PK | |
| Index2 | numeric(18,0) | NO | PK | |

---

## dbo.DOC_TYPE_R_DOC_TYPE_GROUP

**Row count:** 60

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_ID | numeric(18,0) | NO | PK | |
| DOC_TYPE_GROUP | numeric(18,0) | NO | PK | |
| DOC_ORDER | numeric(18,0) | YES |  | |

---

## dbo.DocumentChanges

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DocumentId -> dbo.CollaborativeDocuments.DocumentId ($(@{parent_schema=dbo; parent_table=DocumentChanges; parent_column=DocumentId; ref_schema=dbo; ref_table=CollaborativeDocuments; ref_column=DocumentId; constraint_name=FK_DocumentChanges_CollaborativeDocuments}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ChangeId | nvarchar(128) | NO | PK | |
| DocumentId | nvarchar(128) | NO |  | |
| UserId | nvarchar(128) | YES |  | |
| ClientId | nvarchar(128) | YES |  | |
| Version | int | NO |  | |
| ClientVersion | int | NO |  | |
| ClientSequence | int | NO |  | |
| OperationType | nvarchar(50) | YES |  | |
| Position | int | NO |  | |
| Content | nvarchar(-1) | YES |  | |
| Length | int | YES |  | |
| UserDisplayName | nvarchar(256) | YES |  | |
| CreatedAt | datetime2 | NO |  | |

---

## dbo.DocumentKeyIndexs

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| IndexId | numeric(18,0) | NO | PK | |
| IndexValue | nvarchar(255) | NO |  | |

---

## dbo.DocumentLabels

**Row count:** 28

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| crdate | datetime | NO |  | |
| udate | datetime | NO |  | |
| userid | numeric(18,0) | NO | PK | |
| doctypeid | numeric(18,0) | NO | PK | |
| docid | numeric(18,0) | NO | PK | |
| importance | int | NO |  | |
| favorite | int | NO |  | |

---

## dbo.DocumentsIndexs

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| IndexId | numeric(18,0) | NO | PK | |
| IndexValue | nvarchar(255) | NO |  | |

---

## dbo.DocumentsToInsertStatus

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Name | nvarchar(50) | NO |  | |

---

## dbo.DocumentsUpdateStatus

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Name | nvarchar(50) | NO |  | |

---

## dbo.DocumentUpdateIndexs

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| IndexId | numeric(18,0) | NO | PK | |
| IndexValue | nvarchar(255) | NO |  | |

---

## dbo.DocumentVersions

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DocumentId -> dbo.CollaborativeDocuments.DocumentId ($(@{parent_schema=dbo; parent_table=DocumentVersions; parent_column=DocumentId; ref_schema=dbo; ref_table=CollaborativeDocuments; ref_column=DocumentId; constraint_name=FK_DocumentVersions_CollaborativeDocuments}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| VersionId | nvarchar(128) | NO | PK | |
| DocumentId | nvarchar(128) | NO |  | |
| Version | int | NO |  | |
| Content | nvarchar(-1) | YES |  | |
| SavedByUserId | nvarchar(128) | YES |  | |
| SavedByUserDisplayName | nvarchar(256) | YES |  | |
| SavedAt | datetime2 | NO |  | |

---

## dbo.ESTREG

**Row count:** 185

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(4,0) | NO |  | |
| M_NAME | nvarchar(50) | NO |  | |
| W_USER | nvarchar(50) | NO |  | |
| VER | nvarchar(50) | NO |  | |
| UPDATED | datetime | NO |  | |
| LAST_CHECK | datetime | NO |  | |

---

## dbo.EXCEP

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | YES |  | |
| FECHA | datetime | YES |  | |
| ORIGEN | nvarchar(150) | YES |  | |
| DESCRIPCION | nvarchar(1000) | YES |  | |
| OBSERVACIONES | nvarchar(1000) | YES |  | |

---

## dbo.Fechas

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID Fecha | numeric(18,0) | YES |  | |
| Fecha_ALta | datetime | YES |  | |
| Fecha_AltaUsr | datetime | YES |  | |
| Fecha_BajaUsr | datetime | YES |  | |

---

## dbo.FILE_COMPRESSION_AUDIT

**Row count:** 217

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | bigint | NO | PK | |
| FILE_NAME | nvarchar(255) | NO |  | |
| FILE_PATH | nvarchar(1024) | NO |  | |
| FILE_EXTENSION | nvarchar(32) | YES |  | |
| SOURCE_SIZE_BYTES | bigint | NO |  | |
| SOURCE_LAST_WRITE_UTC | datetime2 | NO |  | |
| RESULT_SIZE_BYTES | bigint | NO |  | |
| RESULT_LAST_WRITE_UTC | datetime2 | NO |  | |
| STRATEGY_NAME | nvarchar(100) | NO |  | |
| STATUS | nvarchar(50) | NO |  | |
| VERIFICATION_SUCCEEDED | bit | NO |  | |
| MESSAGE | nvarchar(4000) | YES |  | |
| PROCESSED_AT_UTC | datetime2 | NO |  | |

---

## dbo.FRMNOTES

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | decimal(10,0) | NO |  | |
| NOMBRE | varchar(50) | NO |  | |

---

## dbo.group_r_group

**Row count:** 465

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserGroup | numeric(18,0) | NO | PK | |
| inheritedUserGroup | numeric(18,0) | NO | PK | |

---

## dbo.GrupoPorNivel

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| GroupID | numeric(18,0) | YES |  | |
| NivelID | numeric(18,0) | YES |  | |

---

## dbo.HelpApplications

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Application | nvarchar(-1) | YES |  | |
| OrderId | int | NO |  | |

---

## dbo.Hoja1$

**Row count:** 996

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Documento | nvarchar(255) | YES |  | |
| HG | float | YES |  | |

---

## dbo.ILMClientSet

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserId | numeric(18,0) | NO |  | |
| UserName | nvarchar(500) | NO |  | |
| Year | numeric(18,0) | NO |  | |
| Month | numeric(18,0) | NO |  | |
| Day | numeric(18,0) | NO |  | |
| Hour | numeric(18,0) | NO |  | |
| Type | numeric(18,0) | NO |  | |
| UpdateDate | datetime | NO |  | |
| CrDate | datetime | NO |  | |
| Client | nvarchar(150) | NO |  | |
| Server | nvarchar(150) | NO |  | |
| Base | nvarchar(150) | NO |  | |
| CodigoMail | nvarchar(500) | NO |  | |
| doc_id | numeric(18,0) | NO |  | |
| DocTypeId | numeric(18,0) | NO |  | |

---

## dbo.Images

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| filename | varchar(50) | YES |  | |
| description | varchar(50) | YES |  | |
| Type | int | YES |  | |
| ver | int | NO |  | |

---

## dbo.Images_Type

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Type | int | NO | PK | |
| Path | varchar(100) | YES |  | |
| Typedesc | varchar(50) | YES |  | |

---

## dbo.INDEX_LINK

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO | PK | |
| NAME | varchar(20) | NO |  | |
| DESCRIPCION | varchar(200) | YES |  | |

---

## dbo.INDEX_LINK_INFO

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO |  | |
| DATA | varchar(100) | YES |  | |
| FLAG | numeric(1,0) | NO |  | |
| DOCTYPE | numeric(18,0) | NO |  | |
| DOCINDEX | numeric(18,0) | NO |  | |
| NAME | varchar(50) | NO |  | |

---

## dbo.INDEX_R_DOC_TYPE

**Row count:** 638

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| INDEX_ID | numeric(18,0) | NO | PK | |
| DOC_TYPE_ID | numeric(18,0) | NO | PK | |
| ORDEN | numeric(18,0) | NO |  | |
| MUSTCOMPLETE | numeric(18,0) | YES |  | |
| LoadLotus | numeric(18,0) | YES |  | |
| ShowLotus | numeric(18,0) | YES |  | |
| Complete | numeric(18,0) | YES |  | |
| IndexSearch | char(20) | YES |  | |
| DefaultValue | varchar(100) | YES |  | |
| IsDataUnique | decimal(5,0) | YES |  | |
| ISREFERENCED | numeric(18,0) | YES |  | |
| Autocomplete | numeric(18,0) | YES |  | |
| MinValue | varchar(100) | YES |  | |
| MaxValue | varchar(100) | YES |  | |

---

## dbo.INDEX_R_INDEX_TYPE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| INDEX_ID | numeric(18,0) | YES |  | |
| INDEX_TYPE_ID | numeric(18,0) | YES |  | |
| ORDEN | numeric(18,0) | YES |  | |

---

## dbo.INDEX_TYPE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| INDEX_TYPE_ID | numeric(18,0) | NO | PK | |
| INDEX_TYPE_NAME | nvarchar(20) | YES |  | |

---

## dbo.IP_FOLDER

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| NOMBRE | nvarchar(100) | NO |  | |
| PATH | nvarchar(150) | NO |  | |
| ID | numeric(18,0) | NO | PK | |
| NOMBREMAQUINA | nvarchar(150) | NO |  | |
| SERVICE | numeric(18,0) | YES |  | |
| USER_ID | numeric(18,0) | YES |  | |
| TIMER | numeric(9,0) | YES |  | |

---

## dbo.IP_FOLDER_PREPROCESS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IP_ID | numeric(18,0) | NO | PK | |
| PREPROCESSNAME | varchar(200) | NO |  | |
| PREPROCESSPARAM | varchar(200) | YES |  | |
| ORDERPOS | numeric(2,0) | NO | PK | |

---

## dbo.IP_FOLDERBACKUP

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID_CARPETA | numeric(18,0) | NO |  | |
| ANTES | numeric(18,0) | NO |  | |
| CARPETA_BACKUP | nvarchar(250) | NO |  | |

---

## dbo.IP_FOLDERCONF

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID_CARPETA | numeric(18,0) | NO |  | |
| TIMER | numeric(9,0) | NO |  | |
| DOMINGO | numeric(1,0) | NO |  | |
| LUNES | numeric(1,0) | NO |  | |
| MARTES | numeric(1,0) | NO |  | |
| MIERCOLES | numeric(1,0) | NO |  | |
| JUEVES | numeric(1,0) | NO |  | |
| VIERNES | numeric(1,0) | NO |  | |
| SABADO | numeric(1,0) | NO |  | |
| TIPO_CONF | numeric(3,0) | NO |  | |
| ALARMA | nvarchar(20) | NO |  | |

---

## dbo.IP_HST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IP_ID | numeric(18,0) | YES |  | |
| IPDATE | varchar(30) | YES |  | |
| IPUSERID | numeric(18,0) | YES |  | |
| IPDOCCOUNT | numeric(9,0) | YES |  | |
| IPINDEXCOUNT | numeric(9,0) | YES |  | |
| IPRESULT | numeric(18,0) | YES |  | |
| IPLINESCOUNT | numeric(9,0) | YES |  | |
| IPERRORCOUNT | numeric(9,0) | YES |  | |
| IPPATH | varchar(250) | YES |  | |
| HST_ID | numeric(18,0) | YES |  | |
| IP_HASH | varchar(50) | YES |  | |

---

## dbo.IP_INDEX

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IP_ID | numeric(18,0) | NO | PK | |
| ARRAY_ID | nvarchar(50) | YES |  | |
| INDEX_ID | nvarchar(50) | YES |  | |
| INDEX_ORDER | numeric(4,0) | NO | PK | |

---

## dbo.IP_PREPROCESS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IP_ID | numeric(18,0) | NO |  | |
| PREPROCESSNAME | nvarchar(30) | NO |  | |
| PREPROCESSPARAM | nvarchar(50) | YES |  | |
| ORDERPOS | numeric(2,0) | NO |  | |

---

## dbo.IP_PROCESSTASK

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID_PROCESS | numeric(18,0) | YES |  | |
| DIA | numeric(18,0) | YES |  | |
| HORA | nvarchar(10) | YES |  | |
| MAQUINA | nvarchar(50) | YES |  | |
| ID_GROUP | nvarchar(10) | YES |  | |
| USER_ID | numeric(18,0) | YES |  | |
| GRUPO_PROCESS | char(1) | YES |  | |

---

## dbo.IP_RESULTS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RESULT_ID | numeric(18,0) | NO |  | |
| RESULT | nvarchar(20) | YES |  | |

---

## dbo.IP_TASK

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| ID_CONFIGURACION | numeric(18,0) | NO |  | |
| FILE_PATH | nvarchar(100) | NO |  | |
| ZIP_ORIGEN | nvarchar(100) | YES |  | |
| BLOQUEADO | numeric(1,0) | NO |  | |
| USUARIO | nvarchar(100) | YES |  | |
| MAQUINA | nvarchar(100) | NO |  | |
| PathOrigen | nvarchar(255) | YES |  | |
| DateCreated | datetime | YES |  | |

---

## dbo.IP_TGRP

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO |  | |
| NAME | nvarchar(50) | NO |  | |
| DESCRIPTION | nvarchar(50) | YES |  | |
| ENABLED | numeric(4,0) | YES |  | |

---

## dbo.IP_TYPE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IP_ID | numeric(18,0) | NO | PK | |
| IP_NAME | nvarchar(100) | NO |  | |
| IP_PATH | nvarchar(250) | NO |  | |
| IP_CHR | nvarchar(10) | NO |  | |
| IP_DOCTYPEID | numeric(18,0) | NO |  | |
| IP_MOVE | numeric(18,0) | NO |  | |
| IP_VERIFY | numeric(18,0) | NO |  | |
| IP_ACEPTBLANK | numeric(18,0) | NO |  | |
| IP_BACKUP | numeric(18,0) | NO |  | |
| IP_DELSOURCE | numeric(18,0) | NO |  | |
| IP_SOURCEVARIABLE | numeric(18,0) | NO |  | |
| IP_MULTIPLEFILES | numeric(18,0) | NO |  | |
| IP_MULTIPLECHR | nvarchar(10) | YES |  | |
| IP_GROUP | numeric(18,0) | NO |  | |
| IP_BACKUPPATH | nvarchar(250) | NO |  | |
| IP_CHECKBATCH | numeric(18,0) | YES |  | |

---

## dbo.IPJOB_LIST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| NAME | varchar(40) | NO |  | |

---

## dbo.IPJOB_R_IPJOB_LIST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IPJOB_LIST_ID | numeric(28,0) | NO |  | |
| IPJOB_ID | numeric(10,0) | NO |  | |

---

## dbo.LCK

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| USER_ID | numeric(18,0) | NO |  | |
| LCK_DATE | datetime | NO |  | |

---

## dbo.LIC

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| NUMERO_LICENCIAS | nvarchar(40) | YES |  | |
| Type | int | YES |  | |
| Nombre | varchar(4) | YES |  | |
| Used | nvarchar(50) | YES |  | |

---

## dbo.LICTYPE

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | numeric(18,0) | YES |  | |
| tipo | char(10) | YES |  | |

---

## dbo.LIQUIDADORESEXTERNOS

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| NOMBRE | nvarchar(100) | NO |  | |
| Email | nvarchar(75) | NO |  | |

---

## dbo.LOTUSINDEXMAPPING

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IndexId | bigint | YES |  | |
| IndexName | varchar(200) | YES |  | |
| orderp | int | YES |  | |

---

## dbo.LSTDID

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| LASTDOCID | numeric(4,0) | NO |  | |

---

## dbo.LSTFNAME

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| LASTNAME | nvarchar(8) | NO |  | |

---

## dbo.MESSAGE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MSG_ID | int | NO | PK | |
| MSG_FROM | int | YES |  | |
| MSG_BODY | nvarchar(4000) | YES |  | |
| SUBJECT | nvarchar(100) | YES |  | |
| MSG_DATE | datetime | YES |  | |
| REENVIO | int | YES |  | |
| DELETED | int | YES |  | |

---

## dbo.MODULE_RIGHTS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MODULO | nvarchar(100) | YES |  | |
| VALOR | nvarchar(200) | YES |  | |

---

## dbo.MODULESRIGHTS

**Row count:** 18

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| MODULO | nvarchar(50) | YES |  | |
| VALOR | nvarchar(150) | YES |  | |

---

## dbo.MSG_ATTACH

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MSG_ID | numeric(10,0) | NO |  | |
| DOC_ID | numeric(10,0) | NO |  | |
| DOC_TYPE_ID | numeric(10,0) | NO |  | |
| FOLDER_ID | numeric(10,0) | NO |  | |
| INDEX_ID | numeric(10,0) | NO |  | |
| NAME | varchar(255) | YES |  | |
| ICON | numeric(10,0) | NO |  | |
| volumelistid | numeric(10,0) | YES |  | |
| DOC_FILE | varchar(255) | YES |  | |
| Offset | numeric(10,0) | YES |  | |
| Disk_Vol_Path | nvarchar(250) | YES |  | |

---

## dbo.MSG_DEST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MSG_ID | numeric(10,0) | NO |  | |
| USER_ID | numeric(10,0) | NO |  | |
| DEST_TYPE | numeric(18,0) | NO |  | |
| READ | bit | NO |  | |
| DELETED | bit | NO |  | |
| USER_NAME | varchar(40) | NO |  | |

---

## dbo.MSG_DEST_TYPE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MSG_TYPE | numeric(18,0) | NO |  | |
| TYPE_TEXT | nvarchar(20) | NO |  | |

---

## dbo.NOTESINDEX_R_DOC_INDEX

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| INDEXID | int | NO |  | |
| CARGAR | int | NO |  | |

---

## dbo.OBJECTID

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| OBJECTTYPE | nvarchar(50) | YES |  | |
| OBJECTTYPEID | numeric(4,0) | NO |  | |

---

## dbo.OBJECTTYPES

**Row count:** 78

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| OBJECTTYPES | nvarchar(70) | NO |  | |
| OBJECTTYPESID | decimal(4,0) | NO |  | |

---

## dbo.OBJLASTID

**Row count:** 46

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| OBJECT_TYPE_ID | numeric(4,0) | NO | PK | |
| OBJECTID | numeric(10,0) | YES |  | |

---

## dbo.OBJLASTIDBKP

**Row count:** 44

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| OBJECT_TYPE_ID | numeric(4,0) | NO |  | |
| OBJECTID | numeric(10,0) | YES |  | |

---

## dbo.OUT_MAP_ATTACH_IDS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MailDocTypeId | numeric(18,0) | NO | PK | |
| AttachDocTypeId | numeric(18,0) | NO |  | |

---

## dbo.OUT_MAP_DOC_TYPE

**Row count:** 32

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_ID | numeric(10,0) | NO |  | |
| MAIL_ATTRIB | numeric(10,0) | NO |  | |
| INDEX_ID | numeric(10,0) | NO |  | |

---

## dbo.OUT_MAP_FOLDER

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ENTRYID | nvarchar(4000) | NO |  | |
| FOLDERNAME | nvarchar(4000) | NO |  | |
| USERID | numeric(10,0) | NO |  | |
| DOCTYPEID | numeric(10,0) | NO |  | |
| MACHINENAME | nvarchar(500) | YES |  | |

---

## dbo.P_HST

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO |  | |
| PROCESS_DATE | datetime | NO |  | |
| USER_ID | numeric(10,0) | NO |  | |
| TOTALFILES | numeric(10,0) | NO |  | |
| PROCESSEDFILES | numeric(10,0) | NO |  | |
| RESULT_ID | numeric(10,0) | NO |  | |
| SKIPEDFILES | numeric(10,0) | NO |  | |
| ERRORFILES | numeric(10,0) | NO |  | |
| PATH | varchar(250) | NO |  | |
| PROCESS_ID | numeric(10,0) | NO |  | |
| HASH | varchar(50) | YES |  | |
| LOGFILE | varchar(250) | YES |  | |
| ERRORFILE | varchar(250) | YES |  | |
| TEMPFILE | varchar(250) | YES |  | |

---

## dbo.PreProcessIDs

**Row count:** 41

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PreprocessName | nvarchar(50) | NO | PK | |
| PreprocessID | numeric(18,0) | NO |  | |

---

## dbo.PRJ_R_O

**Row count:** 411

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PRJID | numeric(18,0) | NO | PK | |
| OBJID | numeric(18,0) | NO | PK | |
| OBJTYP | numeric(18,0) | NO | PK | |

---

## dbo.PRJ_TBL

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Prj_ID | numeric(18,0) | NO | PK | |
| Name | varchar(50) | YES |  | |
| Description | varchar(500) | YES |  | |

---

## dbo.PRO

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PRO_ID | int | NO | PK | |
| PRO_QUERY | varchar(200) | NO |  | |
| PRO_STATE | int | NO |  | |
| PRO_TYPE | smallint | NO |  | |
| PRO_RESULTADO | nvarchar(100) | YES |  | |

---

## dbo.RemoteInsert

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TemporaryID | numeric(10,0) | NO | PK | |
| DocumentName | nvarchar(255) | NO |  | |
| DocTypeId | numeric(18,0) | NO |  | |
| SerializedFile | image(2147483647) | YES |  | |
| FileExtension | nvarchar(5) | YES |  | |
| Status | int | NO |  | |
| Information | nvarchar(2500) | YES |  | |
| DocumentId | numeric(18,0) | NO |  | |
| RegisterDate | datetime | NO |  | |
| InsertDate | datetime | YES |  | |

---

## dbo.remoteinsert_relations

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RelationID | numeric(38,0) | NO | PK | |
| DocTypeID | numeric(38,0) | NO | PK | |
| IndexID | numeric(38,0) | NO |  | |
| Position | numeric(38,0) | NO |  | |
| Name | varchar(200) | YES |  | |

---

## dbo.RemoteServers

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| D | int | YES |  | |

---

## dbo.RemoteUpdate

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TemporaryID | numeric(10,0) | NO | PK | |
| DocTypeId | numeric(18,0) | NO |  | |
| Status | int | NO |  | |
| Information | nvarchar(4000) | YES |  | |

---

## dbo.RemovedActivity

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WorkflowInstanceInternalId | bigint | NO |  | |
| WorkflowInstanceEventId | bigint | NO |  | |
| QualifiedName | nvarchar(128) | NO |  | |
| ParentQualifiedName | nvarchar(128) | YES |  | |
| RemovedActivityAction | nvarchar(2000) | YES |  | |
| Order | int | YES |  | |

---

## dbo.ReportBuilder

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Name | varchar(100) | NO |  | |
| Tables | varchar(255) | NO |  | |
| Fields | varchar(2000) | NO |  | |
| Relations | varchar(1000) | YES |  | |
| Conditions | varchar(1000) | YES |  | |
| SortExpression | varchar(1000) | YES |  | |
| GroupExpression | varchar(200) | YES |  | |

---

## dbo.ReportBuilder_ChartOption

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ReportID | numeric(18,0) | NO | PK | |
| XValueColumn | varchar(2000) | NO |  | |
| YValueColumn | varchar(2000) | NO |  | |
| ChartType | int | NO |  | |

---

## dbo.reportbuilder_linkOption

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| RelationalField | nvarchar(500) | NO |  | |
| EntityType | nvarchar(500) | YES |  | |
| Description | nvarchar(500) | YES |  | |
| Position | bit | NO |  | |

---

## dbo.ReportDecodeValues

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| code | varchar(100) | NO | PK | |
| decode | varchar(500) | YES |  | |

---

## dbo.Reporte_General

**Row count:** 100

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| Query | nvarchar(4000) | YES |  | |
| Name | varchar(100) | YES |  | |
| Completar | varchar(300) | YES |  | |
| GroupExpression | varchar(200) | YES |  | |

---

## dbo.REPORTE_ID

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | YES |  | |
| DES | nvarchar(100) | YES |  | |
| AUTOMATICO | int | YES |  | |

---

## dbo.ReporteGeneral_Rule

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | bigint | NO | PK | |
| ReportId | bigint | NO |  | |
| RuleId | bigint | NO |  | |

---

## dbo.RequestAction

**Row count:** 22

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| RequestDate | datetime | NO |  | |
| FinishDate | datetime | YES |  | |
| IsFinished | bit | NO |  | |
| RequestUserId | numeric(18,0) | NO |  | |
| RequestName | nvarchar(50) | YES |  | |

---

## dbo.RequestActionExecution

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RequestActionId | numeric(18,0) | NO | PK | |
| UserId | numeric(18,0) | NO | PK | |
| StepId | numeric(18,0) | NO | PK | |
| TaskId | numeric(18,0) | NO | PK | |
| RuleId | numeric(18,0) | NO | PK | |
| ExecutionDate | datetime | NO |  | |

---

## dbo.RequestActionRules

**Row count:** 66

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RequestActionId | numeric(18,0) | NO | PK | |
| RuleId | numeric(10,0) | NO | PK | |

---

## dbo.RequestActionTasks

**Row count:** 22

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RequestActionId | numeric(18,0) | NO | PK | |
| TaskId | decimal(18,0) | NO | PK | |
| StepId | numeric(18,0) | NO | PK | |

---

## dbo.RequestActionUsers

**Row count:** 48

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RequestActionId | numeric(18,0) | NO | PK | |
| UserId | numeric(18,0) | NO | PK | |

---

## dbo.Resultados

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | decimal(18,0) | NO |  | |
| Query | nvarchar(1000) | NO |  | |
| Name | varchar(100) | YES |  | |
| Completar | varchar(300) | YES |  | |

---

## dbo.RIGHTSTYPE

**Row count:** 154

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RIGHTSTYPE | nvarchar(50) | YES |  | |
| RIGHTSTYPEID | decimal(4,0) | NO | PK | |

---

## dbo.Schedule

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TASK_ID | int | NO |  | |
| FECHA | datetime | NO |  | |
| FRECUENCIA | int | NO |  | |
| FECHA_FIN | datetime | NO |  | |
| ULTIMA | datetime | NO |  | |
| ACTIVO | bit | NO |  | |
| REPORTE | int | NO |  | |
| FORMATO | int | NO |  | |
| CARPETA | varchar(100) | NO |  | |
| MAIL_TO | varchar(255) | NO |  | |
| MAIL_CC | varchar(255) | NO |  | |
| MAIL_CCO | varchar(255) | NO |  | |
| IMPRIMIR | int | YES |  | |
| MINUTOS | int | YES |  | |
| DESCRIP | nvarchar(100) | YES |  | |
| ORIGEN | int | YES |  | |
| REPET | int | YES |  | |

---

## dbo.Secciones

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdSeccion | numeric(18,0) | NO | PK | |
| nombre | varchar(50) | NO |  | |

---

## dbo.Security

**Row count:** 319

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Fecha | datetime | YES |  | |
| userid | int | YES |  | |
| userpassword | nvarchar(60) | YES |  | |

---

## dbo.SLST_S156664

**Row count:** 15

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(200) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## dbo.Tabla

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Data | nvarchar(150) | YES |  | |

---

## dbo.Tmp_Reporte_General

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| Query | nvarchar(500) | NO |  | |
| Name | varchar(100) | YES |  | |
| Completar | varchar(300) | YES |  | |

---

## dbo.TypesofRules

**Row count:** 24

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Type | numeric(18,0) | NO | PK | |
| Description | nvarchar(50) | NO |  | |

---

## dbo.UCM

**Row count:** 34

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(18,0) | YES |  | |
| C_TIME | datetime | YES |  | |
| U_TIME | datetime | YES |  | |
| WINUSER | nvarchar(200) | YES |  | |
| WINPC | nvarchar(200) | YES |  | |
| CON_ID | numeric(4,0) | NO | PK | |
| TIME_OUT | numeric(4,0) | YES |  | |
| TYPE | numeric(10,0) | YES |  | |

---

## dbo.UCM_HST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Fecha_y_Hora | datetime | YES |  | |
| CountConex | numeric(18,0) | YES |  | |
| Type | numeric(18,0) | YES |  | |
| UID | numeric(18,0) | YES |  | |

---

## dbo.UCMCLIENTSSet

**Row count:** 7781

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(18,0) | NO |  | |
| C_TIME | datetime | YES |  | |
| U_TIME | datetime | YES |  | |
| WINUSER | nvarchar(200) | NO |  | |
| WINPC | nvarchar(200) | NO |  | |
| CON_ID | numeric(18,0) | NO |  | |
| TIME_OUT | numeric(18,0) | YES |  | |
| TYPE | numeric(18,0) | NO |  | |
| Client | nvarchar(100) | NO |  | |
| UpdateDate | datetime | NO |  | |
| Server | nvarchar(100) | YES |  | |
| Base | nvarchar(100) | YES |  | |

---

## dbo.USER_HST

**Row count:** 55254

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(18,0) | YES |  | |
| ACTION_DATE | datetime | YES |  | |
| ACTION_ID | numeric(18,0) | NO |  | |
| OBJECT_ID | numeric(18,0) | YES |  | |
| OBJECT_TYPE_ID | numeric(18,0) | YES |  | |
| ACTION_TYPE | numeric(18,0) | YES |  | |
| S_OBJECT_ID | nvarchar(255) | YES |  | |

---

## dbo.USERADMIN

**Row count:** 24

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | decimal(10,0) | NO |  | |
| IDADMIN | decimal(10,0) | NO |  | |
| NOMBRE | varchar(50) | NO |  | |

---

## dbo.USERPARAM

**Row count:** 24

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | decimal(10,0) | NO |  | |
| IDPARAM | decimal(10,0) | NO |  | |
| NOMBRE | varchar(50) | NO |  | |

---

## dbo.USR_R_FORM

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IDFORM | numeric(10,0) | NO |  | |
| IDUSR | numeric(10,0) | NO |  | |

---

## dbo.USR_R_GROUP

**Row count:** 125

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USRID | numeric(18,0) | NO | PK | |
| GROUPID | numeric(10,0) | NO | PK | |

---

## dbo.USR_R_OPENTASK

**Row count:** 2909

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USERID | numeric(18,0) | NO | PK | |
| TASKID | numeric(18,0) | NO | PK | |
| FECHA | datetime | YES |  | |

---

## dbo.USR_RIGHTS

**Row count:** 11765

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| GROUPID | numeric(10,0) | NO | PK | |
| OBJID | numeric(10,0) | NO | PK | |
| RTYPE | numeric(10,0) | NO | PK | |
| ADITIONAL | numeric(10,0) | NO | PK | |

---

## dbo.Usr_Seccion

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| CoLegajo | smallint | NO |  | |
| IDSeccion | decimal(18,0) | NO |  | |

---

## dbo.UsrData

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserID | numeric(18,0) | NO | PK | |
| CoLegajo | smallint | NO |  | |
| CoNivelAutorizacion | smallint | YES |  | |
| NuAgencia | smallint | YES |  | |

---

## dbo.USRGROUP

**Row count:** 159

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO | PK | |
| NAME | varchar(200) | YES |  | |
| DESCRIPCION | varchar(100) | YES |  | |
| STATE | char(4) | YES |  | |

---

## dbo.USRNOTES

**Row count:** 143

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | YES |  | |
| NOMBRE | varchar(100) | YES |  | |
| CONF_PATHREMOTOARCH | varchar(100) | YES |  | |
| CONF_MAILSERVER | varchar(100) | YES |  | |
| CONF_BASEMAIL | varchar(100) | YES |  | |
| CONF_PATHARCH | varchar(100) | YES |  | |
| CONF_VISTAEXPORTACION | varchar(100) | YES |  | |
| CONF_PAPELERA | varchar(100) | YES |  | |
| CONF_NOMARCHTXT | varchar(100) | YES |  | |
| CONF_SEQMSG | numeric(10,0) | YES |  | |
| CONF_SEQATT | numeric(10,0) | YES |  | |
| CONF_LOCKEO | numeric(10,0) | YES |  | |
| CONF_ACUMIMG | numeric(10,0) | YES |  | |
| CONF_LIMIMG | numeric(10,0) | YES |  | |
| CONF_DESTEXT | numeric(10,0) | YES |  | |
| CONF_TEXTOSUBJECT | varchar(100) | YES |  | |
| CONF_BORRAR | varchar(100) | YES |  | |
| CONF_ARCHCTRL | varchar(255) | YES |  | |
| CONF_SCHEDULESEL | varchar(100) | YES |  | |
| CONF_SCHEDULEFIJO | varchar(100) | YES |  | |
| CONF_SCHEDULEVAR | varchar(100) | YES |  | |
| CONF_EJECUTABLE | varchar(255) | YES |  | |
| CONF_NOMUSERNOTES | varchar(255) | YES |  | |
| CONF_NOMUSERRED | varchar(100) | YES |  | |
| CONF_CHARSREEMPSUBJ | varchar(100) | YES |  | |
| LASTRUNTIME | datetime | YES |  | |
| CONF_REINTENTO | numeric(10,0) | YES |  | |
| ACTIVO | numeric(4,0) | YES |  | |
| CONF_SEQIMG | numeric(10,0) | YES |  | |
| Conf_BodyAndAttachsInExportedMails | varchar(5) | YES |  | |

---

## dbo.USRTABLE

**Row count:** 28

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| NAME | varchar(50) | YES |  | |
| PASSWORD | varchar(50) | YES |  | |
| CRDATE | datetime | YES |  | |
| LUPDATE | datetime | YES |  | |
| STATE | numeric(10,0) | YES |  | |
| DESCRIPTION | varchar(60) | YES |  | |
| ADDRESS_BOOK | varchar(255) | YES |  | |
| EXPIRATIONTIME | numeric(10,0) | YES |  | |
| EXPIRATIONDATE | datetime | YES |  | |
| NOMBRES | varchar(50) | YES |  | |
| APELLIDO | varchar(50) | YES |  | |
| CORREO | varchar(50) | YES |  | |
| TELEFONO | varchar(20) | YES |  | |
| PUESTO | varchar(50) | YES |  | |
| FIRMA | varchar(255) | YES |  | |
| FOTO | varchar(255) | YES |  | |
| CONF_BASEMAIL | nvarchar(30) | YES |  | |
| CONF_MAILSERVER | nvarchar(50) | YES |  | |
| CONF_MAILTYPE | int | YES |  | |
| SMTP | nvarchar(50) | YES |  | |
| colegajo | numeric(18,0) | YES |  | |

---

## dbo.VERREG

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(4,0) | NO |  | |
| VER | nvarchar(50) | NO |  | |
| PATH | nvarchar(200) | NO |  | |
| UPDATED | datetime | YES |  | |
| SCRIPTFILE | nvarchar(60) | YES |  | |

---

## dbo.WaitForDocument

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WId | decimal(10,0) | NO | PK | |
| DocTypeID | decimal(4,0) | NO |  | |
| Start | datetime | NO |  | |
| DueTime | decimal(10,0) | NO |  | |
| IndexID | decimal(10,0) | NO |  | |
| DocID | decimal(10,0) | YES |  | |
| RuleID | decimal(10,0) | NO |  | |
| StepID | decimal(10,0) | NO |  | |

---

## dbo.WaitIndex

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WId | decimal(10,0) | NO | PK | |
| IndexId | decimal(10,0) | NO |  | |
| IndexValue | nvarchar(50) | YES |  | |

---

## dbo.WF_DT

**Row count:** 32

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WFId | numeric(10,0) | NO | PK | |
| DocTypeId | numeric(18,0) | NO | PK | |

---

## dbo.WF_Frms

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Step_Id | numeric(18,0) | NO |  | |
| Form_Id | numeric(10,0) | NO | PK | |
| Sort | numeric(10,0) | NO |  | |

---

## dbo.WF_Opt_Sistema

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Fecha | datetime | NO |  | |

---

## dbo.WF_SVMG_ACTION_DESC

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(38,0) | NO | PK | |
| descripcion | nvarchar(4000) | NO |  | |

---

## dbo.WF_SVMG_ACTIONS

**Row count:** 30

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | numeric(38,0) | NO | PK | |
| type | numeric(38,0) | NO |  | |
| action | numeric(38,0) | NO |  | |
| active | numeric(38,0) | NO |  | |

---

## dbo.WF_SVMG_OPT

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| VERSION_ACT | int | YES |  | |
| VERSION_UPD | int | YES |  | |
| PATH_ACTUAL | varchar(1000) | YES |  | |
| PATH_NUEVO | varchar(1000) | YES |  | |
| PATH_BACKUP | varchar(1000) | YES |  | |
| ACTUALIZAR | int | YES |  | |
| ACTUALIZADO | int | YES |  | |
| FECHA_UPD | datetime | YES |  | |
| FORZAR_UPD | int | YES |  | |
| FALLAS | int | YES |  | |
| MON_VERSION_ACT | int | YES |  | |
| MON_VERSION_UPD | int | YES |  | |
| MON_PATH_ACTUAL | varchar(1000) | YES |  | |
| MON_PATH_NUEVO | varchar(1000) | YES |  | |
| MON_PATH_BACKUP | varchar(1000) | YES |  | |
| MON_ACTUALIZAR | int | YES |  | |
| MON_ACTUALIZADO | int | YES |  | |
| MON_FECHA_UPD | datetime | YES |  | |
| MON_FORZAR_UPD | int | YES |  | |
| MON_FALLAS | int | YES |  | |

---

## dbo.WF_SVMG_SERVICES

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(38,0) | NO | PK | |
| descripcion | nvarchar(4000) | NO |  | |

---

## dbo.WFContexts

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WF_ID | numeric(10,0) | NO |  | |
| V_Name | varchar(50) | NO |  | |
| V_Type | varchar(50) | NO |  | |
| V_Value | varchar(400) | NO |  | |

---

## dbo.WFDocument

**Row count:** 1088

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_ID | numeric(18,0) | NO |  | |
| DOC_TYPE_ID | numeric(18,0) | NO |  | |
| Folder_ID | numeric(18,0) | NO |  | |
| step_Id | numeric(18,0) | NO |  | |
| Do_State_ID | numeric(18,0) | NO |  | |
| Name | varchar(250) | NO |  | |
| IconId | numeric(18,0) | NO |  | |
| CheckIn | datetime | NO |  | |
| User_Asigned | numeric(18,0) | YES |  | |
| Exclusive | numeric(18,0) | YES |  | |
| ExpireDate | datetime | NO |  | |
| User_Asigned_By | numeric(18,0) | YES |  | |
| Date_Asigned_By | datetime | YES |  | |
| Task_ID | numeric(18,0) | NO | PK | |
| Task_State_ID | numeric(18,0) | YES |  | |
| Remark | numeric(18,0) | YES |  | |
| Tag | nvarchar(250) | YES |  | |
| work_id | numeric(18,0) | NO |  | |
| LastUpdateDate | datetime | YES |  | |
| Comments | varchar(1000) | YES |  | |

---

## dbo.WFFaces

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| Name | varchar(50) | NO |  | |

---

## dbo.WFIndexAndVariable

**Row count:** 16

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| name | varchar(200) | NO |  | |
| type | varchar(3) | NO |  | |

---

## dbo.WFIndexAndVariable_r_WFRules

**Row count:** 15

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Rule_id | numeric(10,0) | NO | PK | |

---

## dbo.WFIndexAndVariableConfig

**Row count:** 25

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| manual | varchar(1) | NO | PK | |
| name | varchar(2000) | NO | PK | |
| operator | varchar(10) | NO | PK | |
| value | varchar(2000) | NO | PK | |

---

## dbo.WFRuleParamItems

**Row count:** 8558

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Rule_id | numeric(10,0) | NO | PK | |
| Item | numeric(18,0) | NO | PK | |
| Value | varchar(4000) | YES |  | |
| ObjectTypes | numeric(18,0) | YES |  | |

---

## dbo.WFRules

**Row count:** 1929

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| Name | varchar(2000) | YES |  | |
| step_Id | numeric(18,0) | YES |  | |
| Type | numeric(18,0) | NO |  | |
| ParentId | numeric(18,0) | NO |  | |
| ParentType | numeric(18,0) | NO |  | |
| Class | nvarchar(50) | YES |  | |
| Enable | numeric(10,0) | YES |  | |
| Version | numeric(10,0) | YES |  | |
| IconId | numeric(18,0) | YES |  | |

---

## dbo.WFRULES_R_WFRULESTATES

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RULEID | numeric(10,0) | NO |  | |
| STATEID | numeric(10,0) | NO |  | |
| DESCRIPTION | nvarchar(3500) | YES |  | |

---

## dbo.WFRulesHst

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_Id | numeric(10,0) | NO |  | |
| Doc_Type_Id | numeric(10,0) | NO |  | |
| Folder_Id | numeric(10,0) | NO |  | |
| Step_Id | numeric(10,0) | NO |  | |
| Rule_Id | numeric(10,0) | NO |  | |
| Result | numeric(10,0) | NO |  | |
| User_Id | numeric(10,0) | NO |  | |
| Ejecution_Date | datetime | NO |  | |
| Data | nvarchar(400) | YES |  | |

---

## dbo.WFRULESTATES

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(10,0) | NO | PK | |
| DESCRIPTION | nvarchar(50) | NO |  | |

---

## dbo.WFStateHst

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TaskId | numeric(18,0) | NO |  | |
| StateId | numeric(18,0) | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| ModifiedDate | datetime | NO |  | |

---

## dbo.WFStep

**Row count:** 118

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| step_Id | numeric(18,0) | NO | PK | |
| work_id | numeric(10,0) | NO |  | |
| Name | varchar(50) | NO |  | |
| Description | varchar(100) | YES |  | |
| Help | varchar(2000) | YES |  | |
| CreateDate | datetime | NO |  | |
| ImageIndex | numeric(10,0) | NO |  | |
| EditDate | datetime | NO |  | |
| LocationX | decimal(10,0) | NO |  | |
| LocationY | decimal(10,0) | NO |  | |
| max_Hours | numeric(10,0) | NO |  | |
| max_docs | numeric(10,0) | NO |  | |
| StartAtOpenDoc | numeric(18,0) | NO |  | |
| Color | varchar(50) | NO |  | |
| Width | numeric(18,0) | NO |  | |
| Height | numeric(18,0) | NO |  | |
| Width2 | numeric(18,0) | YES |  | |
| Orden | numeric(18,0) | YES |  | |

---

## dbo.WFStepHst

**Row count:** 15512

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_Id | numeric(18,0) | NO |  | |
| FOLDER_Id | numeric(18,0) | NO |  | |
| Step_Name | nvarchar(250) | YES |  | |
| State | nvarchar(50) | YES |  | |
| UserName | nvarchar(250) | YES |  | |
| Accion | nvarchar(250) | NO |  | |
| Fecha | datetime | NO |  | |
| Doc_Name | nvarchar(250) | NO |  | |
| Doc_Type_Name | nvarchar(250) | NO |  | |
| StepId | numeric(18,0) | YES |  | |
| DocTypeId | numeric(18,0) | YES |  | |
| WorkflowId | numeric(18,0) | YES |  | |
| WorkflowName | varchar(250) | YES |  | |

---

## dbo.WFStepOpt

**Row count:** 164

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| StepId | numeric(18,0) | NO | PK | |
| ObjOne | numeric(18,0) | NO | PK | |
| ObjTwo | numeric(18,0) | NO | PK | |
| ObjExtraData | nvarchar(255) | YES |  | |

---

## dbo.WfStepPerformance

**Row count:** 2323

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocumentId | numeric(18,0) | NO |  | |
| DocumentName | nvarchar(2000) | NO |  | |
| CheckedIn | datetime | NO |  | |
| CheckedOut | datetime | NO |  | |
| StepId | numeric(18,0) | NO |  | |
| StepName | varchar(50) | NO |  | |
| ConsumedMinutes | int | NO |  | |
| WorkflowId | numeric(18,0) | NO |  | |
| WorkflowName | nvarchar(50) | NO |  | |
| UserName | nvarchar(50) | YES |  | |

---

## dbo.WFStepStates

**Row count:** 230

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_State_ID | numeric(18,0) | NO | PK | |
| Description | nvarchar(250) | YES |  | |
| Step_Id | numeric(18,0) | NO |  | |
| Name | varchar(50) | NO |  | |
| Initial | numeric(18,0) | NO |  | |

---

## dbo.WFTask_States

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Task_State_Id | numeric(18,0) | NO | PK | |
| Task_State_Name | nvarchar(30) | NO |  | |

---

## dbo.WFWorkflow

**Row count:** 38

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| work_id | numeric(10,0) | NO | PK | |
| Wstat_id | numeric(10,0) | NO |  | |
| Name | varchar(50) | NO |  | |
| Description | varchar(100) | NO |  | |
| Help | varchar(200) | NO |  | |
| CreateDate | datetime | NO |  | |
| EditDate | datetime | NO |  | |
| Refreshrate | numeric(10,0) | NO |  | |
| InitialStepId | numeric(18,0) | NO |  | |

---

## dbo.Z_GroupToNotify

**Row count:** 33

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TypeId | numeric(18,0) | YES |  | |
| DocId | numeric(18,0) | YES |  | |
| UserId | numeric(18,0) | YES |  | |
| ExtraData | varchar(250) | YES |  | |
| GroupId | numeric(18,0) | YES |  | |
| GroupId2 | numeric(18,0) | YES |  | |

---

## dbo.Z_ME_P

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PROCESS_ID | numeric(18,0) | NO | PK | |
| USER_NAME | nvarchar(500) | NO |  | |
| PROCESS_NAME | nvarchar(500) | NO |  | |
| EMAIL | nvarchar(500) | NO |  | |
| USER_ID | numeric(18,0) | YES |  | |
| USER_PASSWORD | nvarchar(500) | NO |  | |
| IS_ACTIVE | numeric(18,0) | YES |  | |

---

## dbo.Z_ME_PD

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PD_ID | numeric(18,0) | NO | PK | |
| IP_ADDRESS | nvarchar(500) | NO |  | |
| FIELD_PORT | numeric(18,0) | YES |  | |
| FIELD_PROTOCOL | nvarchar(500) | NO |  | |
| HAS_FILTERS | numeric(18,0) | YES |  | |
| FILTER_FIELD | nvarchar(500) | NO |  | |
| FILTER_VALUE | nvarchar(500) | NO |  | |
| FILTER_RECENTS | numeric(18,0) | YES |  | |
| FILTER_NOT_READS | numeric(18,0) | YES |  | |
| EXPORT_ATTACHMENTS_SEPARATELY | numeric(18,0) | YES |  | |
| PROCESS_ID | numeric(18,0) | YES |  | |

---

## dbo.Z_ME_PM

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PM_ID | numeric(18,0) | NO | PK | |
| FOLDER_NAME | nvarchar(500) | NO |  | |
| ENTITY_ID | numeric(18,0) | YES |  | |
| SENT_BY | numeric(18,0) | YES |  | |
| FIELD_TO | numeric(18,0) | YES |  | |
| CC | numeric(18,0) | YES |  | |
| CCO | numeric(18,0) | YES |  | |
| SUBJECT | numeric(18,0) | NO |  | |
| FIELD_BODY | numeric(18,0) | YES |  | |
| FIELD_DATE | numeric(18,0) | NO |  | |
| Z_USER | numeric(18,0) | YES |  | |
| CODE_MAIL | numeric(18,0) | YES |  | |
| TYPE_OF_EXPORT | numeric(18,0) | YES |  | |
| AUT_INCREMENT | numeric(18,0) | YES |  | |
| PROCESS_ID | numeric(18,0) | YES |  | |
| FOLDER_NAME_DEST | nvarchar(500) | NO |  | |

---

## dbo.Z_Publish

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PublishId | numeric(18,0) | NO | PK | |
| DocId | numeric(18,0) | NO | PK | |
| UserId | numeric(18,0) | NO |  | |
| PublishDate | datetime | NO |  | |

---

## dbo.Z_SendToUser

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TypeId | numeric(18,0) | YES |  | |
| PublishId | numeric(18,0) | YES |  | |
| UserId | numeric(18,0) | YES |  | |

---

## dbo.ZABMTables

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TableName | varchar(150) | NO |  | |

---

## dbo.ZambaExport

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TemporaryId | numeric(18,0) | NO | PK | |
| DocumentName | nvarchar(255) | NO |  | |
| DocTypeName | nvarchar(255) | NO |  | |
| Link | nvarchar(255) | NO |  | |
| IsExported | bit | NO |  | |

---

## dbo.ZambaExportIndexes

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TemporaryId | numeric(18,0) | NO |  | |
| IndexName | nvarchar(255) | NO |  | |
| IndexTypeName | nvarchar(255) | NO |  | |
| IndexValue | nvarchar(255) | NO |  | |

---

## dbo.ZambaWFService

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WFActionId | numeric(18,0) | NO | PK | |
| DateFromLastUpdate | datetime | NO |  | |
| Description | nchar(100) | YES |  | |
| DateFromLastUpdateAseg | datetime | YES |  | |
| DateFromLastReasegurosMail | datetime | YES |  | |
| DateFromLastResumenServicios | datetime | YES |  | |

---

## dbo.ZAPP

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID_APP | int | NO | PK | |
| NOMBRE_APP | char(50) | NO |  | |
| ID_ICON | int | NO |  | |
| EXE_APP | varchar(50) | NO |  | |

---

## dbo.Zaprob

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IDU | numeric(18,0) | YES |  | |
| IDT | numeric(18,0) | YES |  | |
| OK | numeric(18,0) | YES |  | |

---

## dbo.ZBarCode

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| FECHA | datetime | NO |  | |
| DOC_TYPE_ID | numeric(18,0) | NO |  | |
| USERID | numeric(18,0) | NO |  | |
| SCANNED | varchar(2) | YES |  | |
| SCANNEDDATE | datetime | YES |  | |
| DOC_ID | numeric(18,0) | YES |  | |
| BATCH | varchar(10) | YES |  | |
| BOX | numeric(18,0) | YES |  | |

---

## dbo.ZBarcodeComplete

**Row count:** 42

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doctypeid | numeric(18,0) | NO |  | |
| Indexid | numeric(18,0) | NO |  | |
| Tabla | varchar(50) | YES |  | |
| Columna | varchar(50) | YES |  | |
| Clave | numeric(18,0) | YES |  | |
| Orden | numeric(18,0) | YES |  | |
| Conditions | varchar(255) | NO |  | |
| WhereCondition | varchar(255) | YES |  | |
| IdWhereCondition | numeric(18,0) | YES |  | |
| IndexGroup | bit | YES |  | |
| FILTERBYINDEX | numeric(9,0) | YES |  | |

---

## dbo.Zbinary

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| BinaryID | numeric(18,0) | NO | PK | |
| BinaryType | int | NO |  | |
| BinaryName | nvarchar(50) | NO |  | |
| UpdateDate | datetime | NO |  | |
| UpdateByUser | int | NO |  | |
| Binary | varbinary(-1) | NO |  | |

---

## dbo.ZButtons

**Row count:** 44

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ButtonID | nvarchar(100) | NO |  | |
| PlaceID | int | NO |  | |
| TypeID | int | NO |  | |
| Params | nvarchar(500) | NO |  | |
| NeedRights | bit | YES |  | |
| ViewClass | nvarchar(50) | YES |  | |
| Caption | nvarchar(100) | NO |  | |
| ButtonOrder | int | NO |  | |
| CreateDate | datetime | YES |  | |
| ModifiedDate | datetime | YES |  | |
| WFID | numeric(18,0) | YES |  | |
| ID | numeric(18,0) | NO | PK | |
| iconId | int | NO |  | |
| GroupName | varchar(50) | YES |  | |
| GroupClass | varchar(50) | YES |  | |

---

## dbo.ZButtonsAndGroups

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Ztype | int | YES |  | |
| Zname | varchar(50) | YES |  | |

---

## dbo.ZButtonsPlace

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| PLACEID | int | NO | PK | |
| PLACEDESC | varchar(50) | NO |  | |

---

## dbo.ZButtonsType

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TYPEID | int | NO | PK | |
| TYPEDESC | varchar(50) | NO |  | |

---

## dbo.ZCARATULAS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ZCARATULAS_ID | int | NO | PK | |
| USRTABLE_ID | int | NO |  | |
| NOMBRE | varchar(50) | YES |  | |
| RUTA | varchar(300) | YES |  | |
| ZCARATULAS_STATES_ID | int | NO |  | |
| FECHA_ULT_MOD | datetime | YES |  | |
| PLANTILLA | varchar(100) | YES |  | |

---

## dbo.ZCARATULAS_STATES

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ZCARATULAS_STATES_ID | int | NO | PK | |
| ESTADO_DESCRIP | varchar(50) | NO |  | |

---

## dbo.ZComment

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOCID | numeric(18,0) | NO |  | |
| COMMENTS | nvarchar(500) | NO |  | |
| CreateDate | datetime | NO |  | |

---

## dbo.ZComparators

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ComparatorID | numeric(18,0) | NO | PK | |
| ComparatorDesc | varchar(50) | NO |  | |

---

## dbo.ZdbtoxmlActions

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| EntityId | numeric(18,0) | YES |  | |
| Description | nvarchar(500) | YES |  | |
| ActionType | numeric(18,0) | NO |  | |
| Filter | nvarchar(300) | NO |  | |
| ObjectId | numeric(18,0) | YES |  | |

---

## dbo.ZDocReads

**Row count:** 391

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USERID | numeric(18,0) | NO | PK | |
| DOCID | numeric(18,0) | NO | PK | |
| DOCTYPEID | numeric(18,0) | NO |  | |
| CRDATE | datetime | NO |  | |

---

## dbo.ZDocRelations

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_Id | numeric(18,0) | NO |  | |
| ParentId | numeric(18,0) | NO |  | |
| RelationId | numeric(18,0) | NO |  | |

---

## dbo.ZDocTypeRights

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| dtId | numeric(18,0) | NO | PK | |
| RightId | numeric(18,0) | NO | PK | |
| RValue | numeric(18,0) | NO |  | |

---

## dbo.ZErrorReportAttachments

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| ReportId | numeric(18,0) | NO |  | |
| FileName | nvarchar(256) | NO |  | |
| Attachment | varbinary(-1) | NO |  | |

---

## dbo.ZErrorReports

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Subject | nvarchar(-1) | YES |  | |
| Description | nvarchar(-1) | NO |  | |
| StateId | int | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| Created | datetime | NO |  | |
| Updated | datetime | NO |  | |
| Comments | nvarchar(-1) | YES |  | |
| WinUser | nvarchar(200) | YES |  | |
| Machine | nvarchar(200) | YES |  | |
| Version | nvarchar(50) | YES |  | |

---

## dbo.ZErrorReportStates

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Description | nvarchar(50) | NO |  | |

---

## dbo.ZExportControl

**Row count:** 25

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Fecha | datetime | YES |  | |
| UserId | nvarchar(30) | YES |  | |
| Line | nvarchar(3000) | YES |  | |
| Insertado | char(1) | YES |  | |
| Codigo | nvarchar(450) | NO |  | |
| Doc_Type_Id | numeric(18,0) | YES |  | |

---

## dbo.ZExportErrors

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Fecha | datetime | YES |  | |
| WinUser | nvarchar(15) | YES |  | |
| MachineName | nvarchar(30) | YES |  | |
| Line | nvarchar(1000) | YES |  | |
| ErrorType | nvarchar(100) | YES |  | |
| Codigo | nvarchar(40) | YES |  | |

---

## dbo.Zext_zfrms

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| zfrms_id | numeric(18,0) | NO | PK | |
| zext_id | numeric(18,0) | NO | PK | |

---

## dbo.zfilters

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Attribute | nvarchar(100) | NO |  | |
| Value | nvarchar(3850) | NO |  | |
| Enabled | numeric(18,0) | NO |  | |
| DataType | numeric(18,0) | NO |  | |
| Comparator | nvarchar(20) | NO |  | |
| FilterType | nvarchar(7) | NO |  | |
| DocTypeId | numeric(18,0) | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| Description | nvarchar(100) | NO |  | |
| IndexDropDown | numeric(18,0) | NO |  | |

---

## dbo.ZForum

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserId | numeric(18,0) | NO |  | |
| IdMensaje | numeric(10,0) | NO | PK | |
| ParentId | numeric(10,0) | NO |  | |
| LinkName | varchar(250) | YES |  | |
| Mensaje | varchar(4000) | YES |  | |
| Fecha | datetime | NO |  | |
| DiasVto | int | YES |  | |

---

## dbo.ZFORUM_R_DOC

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdMensaje | numeric(10,0) | NO | PK | |
| DocId | numeric(18,0) | NO | PK | |
| DocT | numeric(18,0) | NO |  | |

---

## dbo.ZForum_R_USR

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdMensaje | numeric(10,0) | NO | PK | |
| IdUsuario | numeric(18,0) | NO | PK | |

---

## dbo.ZForumAttachBlob

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdMensaje | numeric(10,0) | NO |  | |
| Path | nvarchar(1000) | NO |  | |
| File | varbinary(-1) | YES |  | |
| FileName | nvarchar(200) | YES |  | |

---

## dbo.ZForumAttachs

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdMensaje | numeric(10,0) | NO |  | |
| Path | nvarchar(1000) | NO |  | |

---

## dbo.ZForumMails

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| docid | numeric(10,0) | NO |  | |
| idmensaje | numeric(10,0) | NO |  | |
| parentid | numeric(10,0) | NO |  | |
| mails | nvarchar(1000) | NO |  | |

---

## dbo.ZFrmDesc

**Row count:** 16

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DTId | numeric(18,0) | NO |  | |
| IId | numeric(18,0) | NO |  | |
| Op | nvarchar(50) | NO |  | |
| Value | nvarchar(250) | NO |  | |
| FId | numeric(18,0) | NO |  | |
| QId | numeric(18,0) | NO | PK | |

---

## dbo.zFrmDynamicForms

**Row count:** 441

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdFormulario | numeric(18,0) | NO | PK | |
| IdIndice | numeric(18,0) | NO | PK | |
| IdSeccion | numeric(18,0) | NO |  | |
| NroOrden | numeric(18,0) | NO |  | |
| NroFila | numeric(18,0) | NO |  | |

---

## dbo.ZFrmIndexsDesc

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| FId | numeric(18,0) | NO |  | |
| IId | numeric(18,0) | NO |  | |
| Type | numeric(18,0) | NO |  | |
| Value | nvarchar(50) | NO |  | |
| EId | numeric(18,0) | NO | PK | |

---

## dbo.ZFrms

**Row count:** 85

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Name | nvarchar(50) | YES |  | |
| ParentId | numeric(18,0) | YES |  | |
| Type | numeric(18,0) | YES |  | |
| ObjectTypeId | numeric(18,0) | YES |  | |
| Path | nvarchar(255) | YES |  | |
| Description | nvarchar(250) | YES |  | |
| UseRuleRights | numeric(18,0) | NO |  | |
| Updated | datetime | YES |  | |
| UseBlob | int | YES |  | |
| RebuildForm | tinyint | YES |  | |

---

## dbo.ZFrms_Actions

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ActionID | numeric(18,0) | NO | PK | |
| ActionDesc | varchar(50) | NO |  | |

---

## dbo.zfrms_AdditionalFiles

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Name | nvarchar(200) | NO | PK | |
| Path | nvarchar(200) | NO | PK | |
| blobFile | varbinary(-1) | NO |  | |
| UpdateDate | datetime | NO |  | |

---

## dbo.ZFrms_Conditions

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| FormID | numeric(18,0) | NO |  | |
| ConditionID | numeric(18,0) | NO |  | |
| IndexToValidate | numeric(18,0) | NO |  | |
| Comparator | numeric(18,0) | NO |  | |
| ComparateValue | varchar(100) | YES |  | |
| TargetIndex | numeric(18,0) | NO |  | |
| TargetAction | numeric(18,0) | NO |  | |

---

## dbo.ZFRMSBLOB

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |

---

## dbo.ZFrmsRequirements

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| FrmId | numeric(18,0) | NO |  | |
| Path | nvarchar(255) | NO |  | |

---

## dbo.ZI

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| InsertID | numeric(18,0) | NO |  | |
| DTID | numeric(18,0) | NO |  | |
| DocID | numeric(18,0) | NO |  | |
| FolderID | numeric(18,0) | NO |  | |
| IDate | datetime | NO |  | |
| RuleID | nvarchar(254) | YES |  | |
| name | nvarchar(250) | YES |  | |
| UserID | numeric(18,0) | NO |  | |

---

## dbo.zicons

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID_ICON | int | NO | PK | |
| ICON_PATH | varchar(500) | NO |  | |

---

## dbo.ZIndexAutoincremental

**Row count:** 62

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Index_Id | numeric(18,0) | NO |  | |
| Doc_Type_Id | numeric(18,0) | NO |  | |
| IndexValue | numeric(18,0) | NO |  | |

---

## dbo.ZindexerState

**Row count:** 4102

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocType | int | YES |  | |
| DocId | int | YES |  | |
| Date | datetime | YES |  | |
| State | int | YES |  | |

---

## dbo.ZIndexHierarchyKey

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO |  | |
| Indice | int | YES |  | |
| IndicePadre | int | YES |  | |
| DataValuesTable | varchar(50) | YES |  | |

---

## dbo.ZIndexReference

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DTId | numeric(18,0) | NO |  | |
| IId | numeric(18,0) | NO |  | |
| IServer | nvarchar(50) | YES |  | |
| IDataBase | nvarchar(50) | YES |  | |
| IUser | nvarchar(50) | YES |  | |
| ITable | nvarchar(50) | NO |  | |
| IColumn | nvarchar(50) | NO |  | |
| ReferenceId | numeric(18,0) | NO | PK | |

---

## dbo.ZIndexReferenceKeys

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ReferenceId | numeric(18,0) | NO |  | |
| IndexId | numeric(18,0) | NO |  | |
| IColumn | nvarchar(50) | NO |  | |

---

## dbo.ZIR

**Row count:** 2801

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IndexId | numeric(18,0) | NO |  | |
| DoctypeId | numeric(18,0) | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| RightType | numeric(4,0) | NO |  | |

---

## dbo.ZLS

**Row count:** 21

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| SearchId | numeric(18,0) | NO | PK | |
| UserId | numeric(18,0) | NO |  | |
| Name | nvarchar(-1) | NO |  | |

---

## dbo.ZLSQ

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| SearchId | numeric(18,0) | NO |  | |
| Query | nvarchar(-1) | NO |  | |

---

## dbo.ZMachineConfig

**Row count:** 525

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| MachineName | varchar(300) | NO | PK | |
| name | varchar(200) | NO | PK | |
| Value | varchar(2000) | NO |  | |
| Section | numeric(18,0) | NO | PK | |

---

## dbo.ZMAIL_HISTORY

**Row count:** 118

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| FECHA | datetime | YES |  | |
| USR_ZMB | int | YES |  | |
| USR_PC | varchar(200) | YES |  | |
| DOC_ID | int | YES |  | |
| DOC_TYPE | int | YES |  | |
| MSG_TO | varchar(1000) | YES |  | |
| MSG_CC | varchar(1000) | YES |  | |
| MSG_BCC | varchar(1000) | YES |  | |
| MSG_SUBJECT | varchar(1000) | YES |  | |
| PATH | varchar(1000) | YES |  | |
| EncodeFile | varbinary(-1) | YES |  | |
| ID | numeric(18,0) | NO | PK | |

---

## dbo.ZMailConfig

**Row count:** 131

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserName | varchar(50) | YES |  | |
| UserPassword | varchar(50) | YES |  | |
| ProveedorSMTP | nvarchar(50) | YES |  | |
| Puerto | int | YES |  | |
| Correo | varchar(50) | NO |  | |
| MailServer | nvarchar(50) | YES |  | |
| BaseMail | nvarchar(30) | YES |  | |
| MailType | int | YES |  | |
| UserID | numeric(18,0) | NO | PK | |
| EnableSsl | bit | YES |  | |

---

## dbo.ZMB_SCHEDULER

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| OBJECT_ID | bigint | NO | PK | |
| NAME | varchar(100) | YES |  | |
| TYPE | int | YES |  | |
| ACTIVE | bit | YES |  | |
| START_TIME | datetime | YES |  | |
| STOP_TIME | datetime | YES |  | |
| NEXT_TIME | datetime | YES |  | |
| FROM_TIME | bigint | YES |  | |
| TO_TIME | bigint | YES |  | |
| INTERVAL | bigint | YES |  | |
| SUNDAY | bit | YES |  | |
| MONDAY | bit | YES |  | |
| TUESDAY | bit | YES |  | |
| WEDNESDAY | bit | YES |  | |
| THURSDAY | bit | YES |  | |
| FRIDAY | bit | YES |  | |
| SATURDAY | bit | YES |  | |

---

## dbo.ZMB_WF_Consultas_Documentacion

**Row count:** 41

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO |  | |
| DocID_Consulta | int | NO |  | |
| DocID_Documento | int | NO |  | |

---

## dbo.ZMOD

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | YES |  | |
| NOMBRE | varchar(100) | YES |  | |
| DESCRIPCION | varchar(4000) | YES |  | |

---

## dbo.ZNetronShapes

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Shape_Id | int | NO | PK | |
| Shape_DocT | int | YES |  | |
| Shape_DocId | int | YES |  | |
| Shape_Tipo | smallint | NO |  | |
| Shape_Height | int | YES |  | |
| Shape_Color | int | YES |  | |
| Shape_Opaque | int | YES |  | |
| Shape_Text | nvarchar(255) | YES |  | |
| Shape_Width | int | YES |  | |
| Shape_X | int | YES |  | |
| Shape_Y | int | YES |  | |
| Shape_StartId | smallint | YES |  | |
| Shape_EndId | smallint | YES |  | |
| Shape_StartNum | tinyint | YES |  | |
| Shape_EndNum | tinyint | YES |  | |

---

## dbo.ZNews

**Row count:** 492

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| NewsID | numeric(18,0) | NO | PK | |
| DocID | numeric(18,0) | NO |  | |
| DocTypeID | numeric(18,0) | NO |  | |
| Value | varchar(2000) | NO |  | |
| crdate | datetime | NO |  | |

---

## dbo.ZNewsUsers

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| NewsID | numeric(18,0) | NO |  | |
| UserID | numeric(18,0) | NO |  | |
| Status | int | NO |  | |

---

## dbo.ZObjCount

**Row count:** 149

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ObjectTypeId | numeric(18,0) | NO | PK | |
| ObjectId | numeric(18,0) | NO | PK | |
| ObjectCount | numeric(18,0) | NO |  | |
| FilterId | numeric(18,0) | NO |  | |
| UpdateDate | datetime | YES |  | |

---

## dbo.ZOBS_139103_149657

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| DOC_ID | int | YES |  | |
| USER_ID | varchar(200) | YES |  | |
| DateObs | date | YES |  | |
| VALUE | varchar(200) | YES |  | |

---

## dbo.Zopt

**Row count:** 247

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Item | nvarchar(50) | NO | PK | |
| Value | nvarchar(-1) | NO |  | |

---

## dbo.ZPAQ

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Numero | numeric(18,0) | NO |  | |
| Name | nvarchar(100) | NO |  | |
| Installed | numeric(18,0) | NO |  | |
| Dateinstalled | nvarchar(50) | YES |  | |

---

## dbo.ZparamsItemMail

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | numeric(18,0) | NO |  | |
| idUser | numeric(18,0) | YES |  | |
| docId | numeric(18,0) | YES |  | |
| docTypeId | numeric(18,0) | YES |  | |
| toSend | nvarchar(500) | YES |  | |
| subject | nvarchar(4000) | YES |  | |

---

## dbo.ZPRINT

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| CDATE | datetime | NO |  | |
| USERID | int | NO |  | |
| DOCID | int | NO |  | |
| DOCTYPE | int | NO |  | |
| DOCFILE | varchar(100) | NO |  | |
| MOVED | int | NO |  | |
| MOVED_DATE | datetime | YES |  | |

---

## dbo.ZQCOLUMNS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO |  | |
| SELECTTABLE | nvarchar(50) | NO |  | |
| SELECTCOLUMNS | nvarchar(50) | NO |  | |
| VALUE | nvarchar(254) | YES |  | |

---

## dbo.ZQKEYS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| CLAVES | nvarchar(50) | NO |  | |
| VALUE | nvarchar(254) | YES |  | |
| SELECTTABLE | nvarchar(50) | YES |  | |

---

## dbo.ZQSENTENCE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | int | YES |  | |
| Value | nvarchar(200) | YES |  | |
| type | nvarchar(30) | YES |  | |

---

## dbo.ZQUERYNAME

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO |  | |
| NOMBRE | nvarchar(50) | NO |  | |
| SERVERNAME | nvarchar(15) | NO |  | |
| USUARIO | nvarchar(15) | NO |  | |
| CLAVE | nvarchar(50) | NO |  | |
| SERVERTYPE | int | NO |  | |
| DB | nvarchar(15) | YES |  | |
| querytype | nvarchar(20) | YES |  | |

---

## dbo.ZRelations

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RelationId | numeric(18,0) | NO |  | |
| Name | nvarchar(100) | NO |  | |

---

## dbo.ZRelationsIndexs

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RelationId | numeric(18,0) | NO |  | |
| IndexIdParent | numeric(18,0) | NO |  | |
| IndexIdChild | numeric(18,0) | NO |  | |
| DTIdParent | numeric(18,0) | NO |  | |
| DTIdChild | numeric(18,0) | NO |  | |

---

## dbo.ZRuleOptBase

**Row count:** 3872

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| RULEID | numeric(10,0) | NO |  | |
| SECTIONID | numeric(18,0) | NO |  | |
| OBJECTID | numeric(18,0) | NO |  | |
| OBJEXTRADATA | nvarchar(2000) | YES |  | |
| OBJVALUE | numeric(18,0) | NO |  | |
| ID | numeric(18,0) | NO | PK | |

---

## dbo.ZSCripts

**Row count:** 496

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| name | varchar(500) | NO |  | |
| user | varchar(500) | NO |  | |
| Resultado | varchar(500) | YES |  | |
| fecha | datetime | YES |  | |

---

## dbo.ZSearchDictionary

**Row count:** 139

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Word | nvarchar(255) | NO |  | |

---

## dbo.ZSearchValues

**Row count:** 5516

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Word | nvarchar(255) | NO |  | |

---

## dbo.ZSearchValues_DT

**Row count:** 21191

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WordId | numeric(18,0) | YES |  | |
| IndexId | numeric(18,0) | YES |  | |
| ResultId | numeric(18,0) | YES |  | |
| DTID | numeric(18,0) | YES |  | |

---

## dbo.ZSecciones

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| IdSeccion | numeric(18,0) | NO | PK | |
| nombre | varchar(50) | NO |  | |

---

## dbo.ZSecOption

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| sameuser | nvarchar(5) | YES |  | |
| expired | int | YES |  | |
| acceptnull | nvarchar(5) | YES |  | |
| lockuser | nvarchar(5) | YES |  | |
| samepc | nvarchar(5) | YES |  | |
| SecurityMail | nvarchar(50) | YES |  | |
| LongitudMinima | nvarchar(5) | YES |  | |
| Alfanumerico | nvarchar(5) | YES |  | |
| RepeatPassword | nvarchar(5) | YES |  | |

---

## dbo.ZSer

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocId | int | NO | PK | |
| DocTypeId | int | NO | PK | |
| Pss | varchar(100) | YES |  | |
| Encstate | numeric(18,0) | YES |  | |

---

## dbo.ZService

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| serviceid | numeric(38,0) | NO | PK | |
| type | numeric(38,0) | NO |  | |
| name | varchar(200) | NO |  | |
| description | varchar(500) | NO |  | |

---

## dbo.ZServiceActions

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | numeric(38,0) | NO | PK | |
| serviceid | numeric(38,0) | NO |  | |
| actionid | numeric(38,0) | NO |  | |
| active | numeric(38,0) | NO |  | |

---

## dbo.ZServiceActionsDesc

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| actionid | numeric(38,0) | NO | PK | |
| descripcion | nvarchar(4000) | NO |  | |

---

## dbo.ZServiceDates

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| serviceid | numeric(38,0) | NO | PK | |
| instalationDate | datetime | NO |  | |
| lastRunDate | datetime | NO |  | |

---

## dbo.ZServiceOptions

**Row count:** 122

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| serviceid | numeric(38,0) | NO |  | |
| name | varchar(200) | NO |  | |
| value | varchar(200) | NO |  | |

---

## dbo.ZServiceTypes

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| typeID | numeric(38,0) | NO | PK | |
| typeName | varchar(200) | NO |  | |

---

## dbo.zservidores

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | numeric(18,0) | NO | PK | |
| nombreservidor | char(20) | YES |  | |
| puerto | numeric(18,0) | YES |  | |
| ip | char(15) | YES |  | |
| objeto | char(15) | YES |  | |
| interfaz | char(15) | YES |  | |
| descripcion | nvarchar(50) | YES |  | |
| svrType | numeric(18,0) | YES |  | |

---

## dbo.ZSiteMap

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| Title | nvarchar(100) | YES |  | |
| URL | varchar(500) | YES |  | |
| ParentID | int | YES |  | |

---

## dbo.ZSO

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Name | nvarchar(500) | NO |  | |
| Image | nvarchar(-1) | YES |  | |
| Status | nvarchar(50) | NO |  | |
| LastModified | datetime | NO |  | |
| LocationX | decimal(18,2) | NO |  | |
| LocationY | decimal(18,2) | NO |  | |
| Width | decimal(18,2) | YES |  | |
| Height | decimal(18,2) | YES |  | |
| Color | nvarchar(50) | YES |  | |

---

## dbo.Zss

**Row count:** 25

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Token | nvarchar(4000) | NO |  | |
| UserId | numeric(18,0) | NO | PK | |
| CreateDate | datetime | YES |  | |
| TokenExpireDate | datetime | YES |  | |
| ConnectionId | numeric(4,0) | NO | PK | |
| OktaAccessToken | nvarchar(4000) | YES |  | |
| OktaIdToken | nvarchar(4000) | YES |  | |

---

## dbo.ZTaskStatus

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| StatusId | decimal(1,0) | YES |  | |
| TaskStatus | nvarchar(30) | YES |  | |

---

## dbo.ZTC_CT

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ObjectTypeID | numeric(18,0) | NO |  | |
| ObjectID | numeric(18,0) | NO |  | |
| VN | numeric(18,0) | NO |  | |
| ParentNode | numeric(18,0) | NO |  | |
| NodeType | numeric(18,0) | NO |  | |
| TestCaseId | numeric(18,0) | NO | PK | |
| Author | numeric(18,0) | NO |  | |
| CreateDate | datetime | NO |  | |
| UpdateDate | datetime | NO |  | |
| NodeName | nvarchar(500) | NO |  | |
| NodeDescription | nvarchar(-1) | NO |  | |
| NodeID | numeric(18,0) | YES |  | |
| TestOrder | numeric(18,0) | YES |  | |

---

## dbo.ZTC_EX

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TestCaseID | numeric(18,0) | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| ExecutionDate | datetime | NO |  | |
| ExecutionResultID | numeric(18,0) | NO |  | |
| Comment | nvarchar(-1) | YES |  | |
| ExecutionId | numeric(18,0) | NO | PK | |
| LastStepExecutionID | numeric(18,0) | NO |  | |

---

## dbo.ZTC_RS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ExecutionResultID | numeric(18,0) | NO | PK | |
| ExecutionResultName | nvarchar(70) | NO |  | |

---

## dbo.ZTC_ST

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| StepTypeID | numeric(18,0) | NO | PK | |
| StepTypeName | nvarchar(70) | NO |  | |

---

## dbo.ZTC_TS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TestCaseID | numeric(18,0) | NO | PK | |
| StepId | numeric(18,0) | NO | PK | |
| StepDescription | nvarchar(-1) | NO |  | |
| StepObservation | nvarchar(-1) | YES |  | |
| StepTypeID | numeric(18,0) | NO |  | |

---

## dbo.ZTC_ZG

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| Name | nvarchar(100) | NO |  | |

---

## dbo.ZTempl

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(10,0) | NO | PK | |
| Name | nvarchar(150) | NO |  | |
| Description | nvarchar(250) | NO |  | |
| Type | numeric(10,0) | NO |  | |
| PAth | nvarchar(260) | NO |  | |

---

## dbo.ZThumb

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_TYPE_ID | numeric(18,0) | NO | PK | |
| DOC_ID | numeric(18,0) | NO | PK | |
| THUMB | nvarchar(-1) | NO |  | |

---

## dbo.ZTRACE

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | YES |  | |
| ModuleName | nvarchar(-1) | YES |  | |
| TraceDate | nvarchar(-1) | YES |  | |
| MachineName | nvarchar(-1) | YES |  | |
| IP | nvarchar(-1) | YES |  | |
| WinUser | nvarchar(-1) | YES |  | |
| TraceTime | numeric(18,0) | YES |  | |
| WorkingSet | numeric(18,0) | YES |  | |
| TraceMessage | nvarchar(-1) | YES |  | |
| Details | nvarchar(-1) | YES |  | |
| TraceLevel | numeric(18,0) | YES |  | |
| OSVersion | nvarchar(-1) | YES |  | |
| UserDomainName | nvarchar(-1) | YES |  | |

---

## dbo.Ztype_Zfrms

**Row count:** 85

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DocType_Id | numeric(18,0) | NO | PK | |
| Form_Id | numeric(18,0) | NO | PK | |

---

## dbo.ZUC_UCS

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| TypeID | numeric(18,0) | NO | PK | |
| StepID | numeric(18,0) | NO | PK | |
| Description | nchar(2000) | NO |  | |

---

## dbo.ZUD

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | numeric(18,0) | NO | PK | |
| Value | nvarchar(3500) | NO |  | |
| UserId | numeric(18,0) | NO |  | |
| DataTypeId | numeric(18,0) | NO |  | |

---

## dbo.ZUDT

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DataTypeId | numeric(18,0) | NO | PK | |
| DataType | nvarchar(500) | NO |  | |

---

## dbo.ZUSER_OR_GROUP

**Row count:** 214

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| UserType | numeric(18,0) | NO |  | |
| Name | varchar(255) | YES |  | |

---

## dbo.ZUSER_OR_GROUPBKP

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| UserType | numeric(18,0) | NO |  | |
| Name | varchar(255) | YES |  | |

---

## dbo.zuserconfig

**Row count:** 352

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserId | numeric(18,0) | NO |  | |
| name | varchar(200) | NO |  | |
| Value | varchar(2000) | NO |  | |
| Section | numeric(18,0) | NO |  | |

---

## dbo.ZV_LEGAJOS_PLAYFT_DOCUMENTOS

**Row count:** 54

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| CUIT | varchar(200) | YES |  | |
| FECHA_Y_HORA_DE_RECEPCION | datetime2 | YES |  | |
| NRO_DE_CLIENTE | bigint | YES |  | |
| NRO_DE_DOCUMENTO | varchar(200) | YES |  | |
| GRUPO_ZAMBA | bigint | YES |  | |
| DELEGACIONES | varchar(60) | YES |  | |
| OBSERVACIONES | varchar(4000) | YES |  | |
| SECTOR | varchar(60) | YES |  | |
| ID_TRAMITE | bigint | YES |  | |
| ANO | varchar(200) | YES |  | |
| USUARIO | varchar(30) | YES |  | |
| NRO_DE_PERSONA | varchar(200) | YES |  | |
| TIPO_DOC_PLA | varchar(60) | YES |  | |
| MOSTRAR | varchar(1) | YES |  | |
| NOMBRE_Y_APELLIDO | varchar(200) | YES |  | |
| FECHA_INGRESO | datetime2 | YES |  | |
| ETAPA | varchar(50) | YES |  | |
| ESTADO | varchar(50) | YES |  | |
| WORKFLOW | nvarchar(50) | YES |  | |
| RUTA | varchar(538) | YES |  | |

---

## dbo.ZVariables

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO | PK | |
| name | varchar(500) | NO |  | |
| value | text(2147483647) | NO |  | |
| environment | varchar(500) | YES |  | |

---

## dbo.Zvars

**Row count:** 63

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| VarName | nvarchar(100) | NO |  | |
| TaskId | numeric(18,0) | NO |  | |
| Value | nvarchar(-1) | NO |  | |
| Id | numeric(18,0) | NO | PK | |

---

## dbo.ZVCR

**Row count:** 25

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| GROUPORUSERID | numeric(18,0) | NO | PK | |
| DOCTYPEID | numeric(18,0) | NO | PK | |
| DOCTYPEPARENTID | numeric(18,0) | NO | PK | |
| COLUMN_NAME | varchar(200) | NO | PK | |

---

## dbo.ZVerConfig

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DataId | numeric(18,0) | NO | PK | |
| DtId | numeric(18,0) | NO |  | |
| IndexId | numeric(18,0) | NO |  | |

---

## dbo.ZVerEv

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DataId | numeric(18,0) | NO | PK | |
| EventId | numeric(18,0) | NO | PK | |
| EvValue | nvarchar(40) | NO |  | |

---

## dbo.ZVerEvents

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| EventId | numeric(18,0) | NO | PK | |
| Event | nvarchar(40) | NO |  | |

---

## dbo.ZVIR

**Row count:** 71

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DoctypeParentId | numeric(9,0) | NO | PK | |
| DoctypeId | numeric(9,0) | NO | PK | |
| IndexId | numeric(9,0) | NO | PK | |
| GID | numeric(9,0) | NO | PK | |
| RightType | numeric(9,0) | NO | PK | |

---

## dbo.ZVW

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | numeric(18,0) | NO |  | |
| NAME | varchar(200) | NO |  | |
| TYPE | numeric(18,0) | NO |  | |
| RID | numeric(18,0) | NO |  | |
| ENABLED | numeric(18,0) | NO |  | |
| CLASS | varchar(400) | NO |  | |

---

## dbo.ZWFI

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WI | numeric(18,0) | NO | PK | |
| DTID | numeric(18,0) | NO |  | |
| RuleID | numeric(18,0) | NO |  | |
| InsertID | numeric(18,0) | YES |  | |

---

## dbo.ZWFII

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| WI | numeric(18,0) | NO | PK | |
| IID | numeric(18,0) | NO | PK | |
| IValue | varchar(50) | NO |  | |

---

## dbo.Zwfopt

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Ruleid | numeric(10,0) | NO | PK | |
| Item | numeric(18,0) | NO | PK | |
| ItemValue | nvarchar(50) | NO | PK | |

---

## zambabpm_RRHH.Areas_UsrGroup

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Area | varchar(100) | NO |  | |
| UsrGroup | varchar(100) | NO |  | |

---

## zambabpm_RRHH.BKP_doc_t146_020626

**Row count:** 24

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO |  | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.BKP_UCM_220526

**Row count:** 974

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(18,0) | YES |  | |
| C_TIME | datetime | YES |  | |
| U_TIME | datetime | YES |  | |
| WINUSER | nvarchar(200) | YES |  | |
| WINPC | nvarchar(200) | YES |  | |
| CON_ID | numeric(4,0) | NO |  | |
| TIME_OUT | numeric(4,0) | YES |  | |
| TYPE | numeric(10,0) | YES |  | |

---

## zambabpm_RRHH.BKP_UCM_240626

**Row count:** 974

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| USER_ID | numeric(18,0) | YES |  | |
| C_TIME | datetime | YES |  | |
| U_TIME | datetime | YES |  | |
| WINUSER | nvarchar(200) | YES |  | |
| WINPC | nvarchar(200) | YES |  | |
| CON_ID | numeric(4,0) | NO |  | |
| TIME_OUT | numeric(4,0) | YES |  | |
| TYPE | numeric(10,0) | YES |  | |

---

## zambabpm_RRHH.BKP_Zopt_240626

**Row count:** 247

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Item | nvarchar(50) | NO |  | |
| Value | nvarchar(-1) | NO |  | |

---

## zambabpm_RRHH.BKP_zuserconfig_270526

**Row count:** 349

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| UserId | numeric(18,0) | NO |  | |
| name | varchar(200) | NO |  | |
| Value | varchar(2000) | NO |  | |
| Section | numeric(18,0) | NO |  | |

---

## zambabpm_RRHH.CalendarEvents

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| calendareventid | int | NO | PK | |
| eventdata | varchar(-1) | YES |  | |
| groupid | int | YES |  | |
| userid | int | YES |  | |

---

## zambabpm_RRHH.CarouselConfig

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| UserId | int | NO |  | |
| DotPosition | varchar(20) | YES |  | |
| AutoPlaySpeed | int | YES |  | |
| AutoPlay | int | YES |  | |
| EnableSwipe | int | YES |  | |
| Loop | int | YES |  | |

---

## zambabpm_RRHH.CarouselContent

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| ContentPath | varchar(500) | NO |  | |
| SourceContent | varchar(50) | NO |  | |
| UserId | int | NO |  | |

---

## zambabpm_RRHH.ChartConfig

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| ChartTypeId | int | NO |  | |
| AttrSelected | nvarchar(50) | NO |  | |
| ChartTitle | nvarchar(100) | YES |  | |
| DimX | int | NO |  | |
| DimY | int | NO |  | |
| PosX | int | NO |  | |
| PosY | int | NO |  | |

---

## zambabpm_RRHH.ChartContainer

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ID | int | NO | PK | |
| DimY | int | NO |  | |
| DimX | int | NO |  | |

---

## zambabpm_RRHH.ChartType

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Descripcion | nvarchar(50) | NO |  | |

---

## zambabpm_RRHH.DashboardUserConfig

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| dashboarduserconfig_id | int | NO | PK | |
| userid | int | NO |  | |
| valueconfig | varchar(-1) | NO |  | |
| value | varchar(-1) | NO |  | |
| group_id | varchar(50) | YES |  | |

---

## zambabpm_RRHH.DashboardUsers

**Row count:** 15

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Enterpriseuser_id | int | NO | PK | |
| companyname | nvarchar(200) | NO |  | |
| firstname | nvarchar(-1) | NO |  | |
| lastname | nvarchar(-1) | NO |  | |
| phonenumber | nvarchar(-1) | NO |  | |
| email | nvarchar(200) | YES |  | |
| password | nvarchar(-1) | NO |  | |
| department_id | int | NO |  | |
| rol_id | int | NO |  | |
| isActive | bit | NO |  | |
| username | nvarchar(-1) | NO |  | |
| userid | int | YES |  | |

---

## zambabpm_RRHH.Departments

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| department_id | int | NO | PK | |
| departmentname | nvarchar(-1) | NO |  | |

---

## zambabpm_RRHH.DOC_B101

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T101.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B101; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T101; ref_column=DOC_ID; constraint_name=FK_DOCT101_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B102

**Row count:** 2

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T102.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B102; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T102; ref_column=DOC_ID; constraint_name=FK_DOCT102_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B103

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T103.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B103; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T103; ref_column=DOC_ID; constraint_name=FK_DOCT103_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B104

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T104.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B104; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T104; ref_column=DOC_ID; constraint_name=FK_DOCT104_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B105

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T105.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B105; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T105; ref_column=DOC_ID; constraint_name=FK_DOCT105_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B107

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T107.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B107; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T107; ref_column=DOC_ID; constraint_name=FK_DOCT107_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B108

**Row count:** 12

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T108.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B108; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T108; ref_column=DOC_ID; constraint_name=FK_DOCT108_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B109

**Row count:** 17

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T109.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B109; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T109; ref_column=DOC_ID; constraint_name=FK_DOCT109_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B110

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T110.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B110; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T110; ref_column=DOC_ID; constraint_name=FK_DOCT110_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B111

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T111.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B111; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T111; ref_column=DOC_ID; constraint_name=FK_DOCT111_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B113

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T113.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B113; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T113; ref_column=DOC_ID; constraint_name=FK_DOCT113_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B114

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T114.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B114; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T114; ref_column=DOC_ID; constraint_name=FK_DOCT114_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B118

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T118.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B118; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T118; ref_column=DOC_ID; constraint_name=FK_DOCT118_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B119

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T119.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B119; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T119; ref_column=DOC_ID; constraint_name=FK_DOCT119_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B120

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T120.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B120; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T120; ref_column=DOC_ID; constraint_name=FK_DOCT120_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B121

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T121.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B121; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T121; ref_column=DOC_ID; constraint_name=FK_DOCT121_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B122

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T122.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B122; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T122; ref_column=DOC_ID; constraint_name=FK_DOCT122_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B123

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T123.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B123; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T123; ref_column=DOC_ID; constraint_name=FK_DOCT123_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B124

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T124.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B124; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T124; ref_column=DOC_ID; constraint_name=FK_DOCT124_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B125

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T125.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B125; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T125; ref_column=DOC_ID; constraint_name=FK_DOCT125_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B126

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T126.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B126; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T126; ref_column=DOC_ID; constraint_name=FK_DOCT126_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B129

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T129.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B129; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T129; ref_column=DOC_ID; constraint_name=FK_DOCT129_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B130

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T130.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B130; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T130; ref_column=DOC_ID; constraint_name=FK_DOCT130_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B131

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T131.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B131; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T131; ref_column=DOC_ID; constraint_name=FK_DOCT131_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B132

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T132.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B132; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T132; ref_column=DOC_ID; constraint_name=FK_DOCT132_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B134

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T134.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B134; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T134; ref_column=DOC_ID; constraint_name=FK_DOCT134_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B135

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T135.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B135; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T135; ref_column=DOC_ID; constraint_name=FK_DOCT135_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B136

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T136.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B136; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T136; ref_column=DOC_ID; constraint_name=FK_DOCT136_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B137

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T137.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B137; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T137; ref_column=DOC_ID; constraint_name=FK_DOCT137_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B138

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T138.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B138; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T138; ref_column=DOC_ID; constraint_name=FK_DOCT138_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B139

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T139.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B139; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T139; ref_column=DOC_ID; constraint_name=FK_DOCT139_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B140

**Row count:** 7

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T140.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B140; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T140; ref_column=DOC_ID; constraint_name=FK_DOCT140_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B142

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T142.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B142; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T142; ref_column=DOC_ID; constraint_name=FK_DOCT142_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B143

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T143.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B143; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T143; ref_column=DOC_ID; constraint_name=FK_DOCT143_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B144

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T144.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B144; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T144; ref_column=DOC_ID; constraint_name=FK_DOCT144_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B145

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T145.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B145; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T145; ref_column=DOC_ID; constraint_name=FK_DOCT145_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B146

**Row count:** 29

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T146.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B146; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T146; ref_column=DOC_ID; constraint_name=FK_DOCT146_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B148

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T148.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B148; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T148; ref_column=DOC_ID; constraint_name=FK_DOCT148_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B149

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T149.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B149; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T149; ref_column=DOC_ID; constraint_name=FK_DOCT149_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B150

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T150.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B150; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T150; ref_column=DOC_ID; constraint_name=FK_DOCT150_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B151

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T151.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B151; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T151; ref_column=DOC_ID; constraint_name=FK_DOCT151_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_B152

**Row count:** 4

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T152.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_B152; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T152; ref_column=DOC_ID; constraint_name=FK_DOCT152_DOCB}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| DOCFILE | varbinary(-1) | NO |  | |
| ZIPPED | int | NO |  | |

---

## zambabpm_RRHH.DOC_I101

**Row count:** 6

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T101.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I101; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T101; ref_column=DOC_ID; constraint_name=FK__DOC_I101__DOC_ID__15BB0E23}.constraint_name))
- I234 -> zambabpm_RRHH.SLST_S234.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I101; parent_column=I234; ref_schema=zambabpm_RRHH; ref_table=SLST_S234; ref_column=Codigo; constraint_name=FK_DOC_T101_SLST_S234}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I103 | numeric(18,0) | YES |  | |
| I111 | datetime | YES |  | |
| I104 | numeric(18,0) | YES |  | |
| I105 | numeric(18,0) | YES |  | |
| I106 | numeric(18,0) | YES |  | |
| I107 | varchar(30) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I117 | varchar(30) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |
| I124 | varchar(30) | YES |  | |
| I126 | numeric(18,0) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I129 | varchar(200) | YES |  | |
| I134 | numeric(18,0) | YES |  | |
| I135 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| i114 | varchar(200) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I234 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i125 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I102

**Row count:** 2

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T102.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I102; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T102; ref_column=DOC_ID; constraint_name=FK__DOC_I102__DOC_ID__23150941}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I137 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I266 | varchar(200) | YES |  | |
| I265 | varchar(2000) | YES |  | |
| I267 | varchar(200) | YES |  | |
| I268 | varchar(30) | YES |  | |
| I214 | datetime | YES |  | |
| I271 | datetime | YES |  | |
| I272 | varchar(30) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I103

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T103.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T103; ref_column=DOC_ID; constraint_name=FK__DOC_I103__DOC_ID__306F045F}.constraint_name))
- I103 -> zambabpm_RRHH.SLST_S103.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=I103; ref_schema=zambabpm_RRHH; ref_table=SLST_S103; ref_column=Codigo; constraint_name=FK_DOC_T103_SLST_S103}.constraint_name))
- I104 -> zambabpm_RRHH.SLST_S104.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=I104; ref_schema=zambabpm_RRHH; ref_table=SLST_S104; ref_column=Codigo; constraint_name=FK_DOC_T103_SLST_S104}.constraint_name))
- I106 -> zambabpm_RRHH.SLST_S106.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=I106; ref_schema=zambabpm_RRHH; ref_table=SLST_S106; ref_column=Codigo; constraint_name=FK_DOC_T103_SLST_S106}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T103_SLST_S118}.constraint_name))
- I218 -> zambabpm_RRHH.SLST_S218.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I103; parent_column=I218; ref_schema=zambabpm_RRHH; ref_table=SLST_S218; ref_column=Codigo; constraint_name=FK_DOC_T103_SLST_S218}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I139 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I140 | numeric(18,0) | NO |  | |
| I141 | numeric(18,0) | NO |  | |
| I142 | numeric(18,0) | NO |  | |
| I111 | datetime | YES |  | |
| I105 | numeric(18,0) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I117 | varchar(30) | YES |  | |
| I124 | varchar(30) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I120 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |
| I126 | numeric(18,0) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I129 | varchar(200) | YES |  | |
| I133 | varchar(200) | YES |  | |
| I177 | varchar(200) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| i114 | varchar(200) | YES |  | |
| I107 | varchar(30) | YES |  | |
| I106 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I123 | numeric(18,0) | YES |  | |
| I218 | numeric(18,0) | YES |  | |
| I191 | numeric(18,0) | YES |  | |
| I231 | numeric(18,0) | YES |  | |
| I103 | numeric(18,0) | YES |  | |
| I104 | numeric(18,0) | YES |  | |
| I236 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i125 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I104

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T104.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I104; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T104; ref_column=DOC_ID; constraint_name=FK__DOC_I104__DOC_ID__3DC8FF7D}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I106 | numeric(18,0) | YES |  | |
| I139 | numeric(18,0) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I143 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I271 | datetime | YES |  | |
| I272 | varchar(30) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I107 | varchar(30) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I105

**Row count:** 5

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T105.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I105; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T105; ref_column=DOC_ID; constraint_name=FK__DOC_I105__DOC_ID__52C41C63}.constraint_name))
- I194 -> zambabpm_RRHH.SLST_S194.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I105; parent_column=I194; ref_schema=zambabpm_RRHH; ref_table=SLST_S194; ref_column=Codigo; constraint_name=FK_DOC_T105_SLST_S194}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I144 | numeric(18,0) | YES |  | |
| I145 | numeric(18,0) | YES |  | |
| I146 | varchar(200) | YES |  | |
| I147 | varchar(200) | YES |  | |
| I148 | datetime | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I194 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I107

**Row count:** 33

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T107.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I107; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T107; ref_column=DOC_ID; constraint_name=FK__DOC_I107__DOC_ID__7148A383}.constraint_name))
- I160 -> zambabpm_RRHH.SLST_S160.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I107; parent_column=I160; ref_schema=zambabpm_RRHH; ref_table=SLST_S160; ref_column=Codigo; constraint_name=FK_DOC_T107_SLST_S160}.constraint_name))
- I240 -> zambabpm_RRHH.SLST_S240.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I107; parent_column=I240; ref_schema=zambabpm_RRHH; ref_table=SLST_S240; ref_column=Codigo; constraint_name=FK_DOC_T107_SLST_S240}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I151 | numeric(18,0) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I153 | numeric(18,0) | YES |  | |
| I154 | varchar(200) | YES |  | |
| I155 | datetime | YES |  | |
| I157 | numeric(18,0) | YES |  | |
| I158 | numeric(18,0) | NO |  | |
| I134 | numeric(18,0) | YES |  | |
| I135 | numeric(18,0) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I184 | varchar(200) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |
| I124 | varchar(30) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I150 | numeric(18,0) | YES |  | |
| i114 | varchar(200) | YES |  | |
| i156 | decimal(18,2) | YES |  | |
| I211 | datetime | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I219 | numeric(18,0) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I112 | datetime | YES |  | |
| I113 | datetime | YES |  | |
| I234 | numeric(18,0) | YES |  | |
| I235 | numeric(18,0) | YES |  | |
| I237 | varchar(30) | YES |  | |
| I240 | numeric(18,0) | YES |  | |
| I232 | numeric(18,0) | YES |  | |
| I216 | numeric(18,0) | YES |  | |
| I192 | numeric(18,0) | YES |  | |
| I246 | numeric(18,0) | NO |  | |
| I248 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i125 | varchar(200) | YES |  | |
| I107 | varchar(30) | YES |  | |
| I126 | numeric(18,0) | YES |  | |
| i212 | numeric(7,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I189 | numeric(18,0) | NO |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I108

**Row count:** 12

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T108.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I108; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T108; ref_column=DOC_ID; constraint_name=FK__DOC_I108__DOC_ID__008AE713}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I159 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I265 | varchar(2000) | YES |  | |
| I266 | varchar(200) | YES |  | |
| I267 | varchar(200) | YES |  | |
| I268 | varchar(30) | YES |  | |
| I271 | datetime | YES |  | |
| I272 | decimal(18,2) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I109

**Row count:** 17

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T109.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I109; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T109; ref_column=DOC_ID; constraint_name=FK__DOC_I109__DOC_ID__5224FDE5}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I185 | numeric(18,0) | YES |  | |
| I186 | numeric(18,0) | YES |  | |
| I158 | numeric(18,0) | NO |  | |
| I189 | numeric(18,0) | NO |  | |
| I136 | varchar(4000) | YES |  | |
| I233 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I263 | numeric(18,0) | YES |  | |
| I214 | datetime | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I190 | numeric(18,0) | NO |  | |
| I247 | numeric(18,0) | NO |  | |
| I246 | numeric(18,0) | NO |  | |
| I245 | numeric(18,0) | NO |  | |
| I286 | datetime | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I110

**Row count:** 10

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T110.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I110; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T110; ref_column=DOC_ID; constraint_name=FK__DOC_I110__DOC_ID__7C1B37B1}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I110 | varchar(30) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| I168 | numeric(18,0) | YES |  | |
| I169 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I189 | numeric(18,0) | NO |  | |
| I224 | numeric(18,0) | YES |  | |
| I192 | numeric(18,0) | YES |  | |
| I227 | datetime | YES |  | |
| I228 | datetime | YES |  | |
| I225 | numeric(18,0) | NO |  | |
| I229 | datetime | YES |  | |
| I230 | datetime | YES |  | |
| I226 | numeric(18,0) | NO |  | |
| I164 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I239 | numeric(18,0) | YES |  | |
| i165 | numeric(7,0) | YES |  | |
| I262 | datetime | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I283 | datetime | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| I295 | numeric(18,0) | YES |  | |
| I167 | numeric(18,0) | YES |  | |
| i170 | varchar(200) | YES |  | |
| I290 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I111

**Row count:** 12

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T111.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I111; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T111; ref_column=DOC_ID; constraint_name=FK__DOC_I111__DOC_ID__28EDDE28}.constraint_name))
- I294 -> zambabpm_RRHH.SLST_S294.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I111; parent_column=I294; ref_schema=zambabpm_RRHH; ref_table=SLST_S294; ref_column=Codigo; constraint_name=FK_DOC_T111_SLST_S294}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I162 | datetime | YES |  | |
| I112 | datetime | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I225 | numeric(18,0) | NO |  | |
| I167 | numeric(18,0) | YES |  | |
| I173 | varchar(200) | YES |  | |
| I149 | numeric(18,0) | YES |  | |
| I169 | numeric(18,0) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| I163 | datetime | YES |  | |
| I164 | numeric(18,0) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I262 | datetime | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I283 | datetime | YES |  | |
| I290 | numeric(18,0) | YES |  | |
| I294 | numeric(18,0) | YES |  | |
| I273 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i170 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I113

**Row count:** 5

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T113.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T113; ref_column=DOC_ID; constraint_name=FK__DOC_I113__DOC_ID__640EA3F6}.constraint_name))
- I121 -> zambabpm_RRHH.SLST_S121.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=I121; ref_schema=zambabpm_RRHH; ref_table=SLST_S121; ref_column=Codigo; constraint_name=FK_DOC_T113_SLST_S121}.constraint_name))
- I152 -> zambabpm_RRHH.SLST_S152.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=I152; ref_schema=zambabpm_RRHH; ref_table=SLST_S152; ref_column=Codigo; constraint_name=FK_DOC_T113_SLST_S152}.constraint_name))
- I153 -> zambabpm_RRHH.SLST_S153.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=I153; ref_schema=zambabpm_RRHH; ref_table=SLST_S153; ref_column=Codigo; constraint_name=FK_DOC_T113_SLST_S153}.constraint_name))
- I204 -> zambabpm_RRHH.SLST_S204.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=I204; ref_schema=zambabpm_RRHH; ref_table=SLST_S204; ref_column=Codigo; constraint_name=FK_DOC_T113_SLST_S204}.constraint_name))
- I235 -> zambabpm_RRHH.SLST_S235.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I113; parent_column=I235; ref_schema=zambabpm_RRHH; ref_table=SLST_S235; ref_column=Codigo; constraint_name=FK_DOC_T113_SLST_S235}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I184 | varchar(200) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I124 | varchar(30) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I177 | varchar(200) | YES |  | |
| I178 | numeric(18,0) | YES |  | |
| I175 | numeric(18,0) | YES |  | |
| I181 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| i114 | varchar(200) | YES |  | |
| I217 | numeric(18,0) | YES |  | |
| I216 | numeric(18,0) | YES |  | |
| I250 | varchar(200) | YES |  | |
| I204 | numeric(18,0) | YES |  | |
| I251 | varchar(200) | YES |  | |
| I252 | varchar(30) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I237 | varchar(30) | YES |  | |
| I253 | varchar(30) | YES |  | |
| I254 | varchar(30) | YES |  | |
| I256 | varchar(30) | YES |  | |
| I255 | varchar(30) | YES |  | |
| I179 | numeric(18,0) | YES |  | |
| I180 | numeric(18,0) | NO |  | |
| i125 | varchar(200) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I235 | numeric(18,0) | YES |  | |
| I153 | numeric(18,0) | YES |  | |
| I129 | varchar(200) | YES |  | |
| I257 | numeric(18,0) | YES |  | |
| I173 | varchar(200) | YES |  | |
| I112 | datetime | YES |  | |
| I113 | datetime | YES |  | |
| I126 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I114

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T114.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I114; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T114; ref_column=DOC_ID; constraint_name=FK__DOC_I114__DOC_ID__2E71AD54}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I182 | numeric(18,0) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I271 | datetime | YES |  | |
| I272 | varchar(30) | YES |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I118

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T118.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T118; ref_column=DOC_ID; constraint_name=FK__DOC_I118__DOC_ID__67AA2AB0}.constraint_name))
- I103 -> zambabpm_RRHH.SLST_S103.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I103; ref_schema=zambabpm_RRHH; ref_table=SLST_S103; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S103}.constraint_name))
- I104 -> zambabpm_RRHH.SLST_S104.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I104; ref_schema=zambabpm_RRHH; ref_table=SLST_S104; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S104}.constraint_name))
- I106 -> zambabpm_RRHH.SLST_S106.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I106; ref_schema=zambabpm_RRHH; ref_table=SLST_S106; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S106}.constraint_name))
- I144 -> zambabpm_RRHH.SLST_S144.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I144; ref_schema=zambabpm_RRHH; ref_table=SLST_S144; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S144}.constraint_name))
- I153 -> zambabpm_RRHH.SLST_S153.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I153; ref_schema=zambabpm_RRHH; ref_table=SLST_S153; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S153}.constraint_name))
- I193 -> zambabpm_RRHH.SLST_S193.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I193; ref_schema=zambabpm_RRHH; ref_table=SLST_S193; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S193}.constraint_name))
- I194 -> zambabpm_RRHH.SLST_S194.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I194; ref_schema=zambabpm_RRHH; ref_table=SLST_S194; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S194}.constraint_name))
- I198 -> zambabpm_RRHH.SLST_S198.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I198; ref_schema=zambabpm_RRHH; ref_table=SLST_S198; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S198}.constraint_name))
- I199 -> zambabpm_RRHH.SLST_S199.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I199; ref_schema=zambabpm_RRHH; ref_table=SLST_S199; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S199}.constraint_name))
- I240 -> zambabpm_RRHH.SLST_S240.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I240; ref_schema=zambabpm_RRHH; ref_table=SLST_S240; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S240}.constraint_name))
- I241 -> zambabpm_RRHH.SLST_S241.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I118; parent_column=I241; ref_schema=zambabpm_RRHH; ref_table=SLST_S241; ref_column=Codigo; constraint_name=FK_DOC_T118_SLST_S241}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I195 | varchar(200) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I103 | numeric(18,0) | YES |  | |
| I111 | datetime | YES |  | |
| I104 | numeric(18,0) | YES |  | |
| I105 | numeric(18,0) | YES |  | |
| I106 | numeric(18,0) | YES |  | |
| I107 | varchar(30) | YES |  | |
| I114 | varchar(200) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I117 | varchar(30) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |
| I124 | varchar(30) | YES |  | |
| I200 | varchar(200) | YES |  | |
| I129 | varchar(200) | YES |  | |
| I203 | varchar(200) | YES |  | |
| I204 | numeric(18,0) | YES |  | |
| I153 | numeric(18,0) | YES |  | |
| I193 | numeric(18,0) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I112 | datetime | YES |  | |
| I113 | datetime | YES |  | |
| I205 | varchar(1) | YES |  | |
| I147 | varchar(200) | YES |  | |
| I144 | numeric(18,0) | YES |  | |
| I194 | numeric(18,0) | YES |  | |
| I145 | numeric(18,0) | YES |  | |
| I146 | varchar(200) | YES |  | |
| I148 | datetime | YES |  | |
| I196 | varchar(200) | YES |  | |
| I197 | numeric(18,0) | YES |  | |
| I198 | numeric(18,0) | YES |  | |
| I199 | numeric(18,0) | YES |  | |
| I201 | decimal(18,2) | YES |  | |
| I202 | varchar(200) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I240 | numeric(18,0) | YES |  | |
| I232 | numeric(18,0) | YES |  | |
| I216 | numeric(18,0) | YES |  | |
| I127 | numeric(18,0) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I242 | numeric(18,0) | NO |  | |
| I244 | varchar(200) | YES |  | |
| I241 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i125 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I119

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T119.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I119; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T119; ref_column=DOC_ID; constraint_name=FK__DOC_I119__DOC_ID__750425CE}.constraint_name))
- I106 -> zambabpm_RRHH.SLST_S106.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I119; parent_column=I106; ref_schema=zambabpm_RRHH; ref_table=SLST_S106; ref_column=Codigo; constraint_name=FK_DOC_T119_SLST_S106}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I207 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I128 | numeric(18,0) | YES |  | |
| I156 | decimal(18,2) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I107 | varchar(30) | YES |  | |
| I106 | numeric(18,0) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I271 | datetime | YES |  | |
| I272 | varchar(30) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I120

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T120.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I120; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T120; ref_column=DOC_ID; constraint_name=FK__DOC_I120__DOC_ID__3C55B857}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I208 | numeric(18,0) | YES |  | |
| I162 | datetime | YES |  | |
| I163 | datetime | YES |  | |
| I209 | numeric(18,0) | YES |  | |
| I210 | decimal(18,2) | YES |  | |
| I189 | numeric(18,0) | NO |  | |
| I136 | varchar(4000) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i170 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I121

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T121.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I121; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T121; ref_column=DOC_ID; constraint_name=FK__DOC_I121__DOC_ID__344A823B}.constraint_name))
- I216 -> zambabpm_RRHH.SLST_S216.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I121; parent_column=I216; ref_schema=zambabpm_RRHH; ref_table=SLST_S216; ref_column=Codigo; constraint_name=FK_DOC_T121_SLST_S216}.constraint_name))
- I218 -> zambabpm_RRHH.SLST_S218.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I121; parent_column=I218; ref_schema=zambabpm_RRHH; ref_table=SLST_S218; ref_column=Codigo; constraint_name=FK_DOC_T121_SLST_S218}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I213 | numeric(18,0) | YES |  | |
| I214 | datetime | YES |  | |
| I215 | decimal(18,2) | YES |  | |
| I216 | numeric(18,0) | YES |  | |
| I217 | numeric(18,0) | YES |  | |
| I218 | numeric(18,0) | YES |  | |
| I190 | numeric(18,0) | NO |  | |
| I245 | numeric(18,0) | NO |  | |
| I246 | numeric(18,0) | NO |  | |
| I136 | varchar(4000) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| i170 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I122

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T122.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I122; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T122; ref_column=DOC_ID; constraint_name=FK__DOC_I122__DOC_ID__35FDC083}.constraint_name))
- I152 -> zambabpm_RRHH.SLST_S152.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I122; parent_column=I152; ref_schema=zambabpm_RRHH; ref_table=SLST_S152; ref_column=Codigo; constraint_name=FK_DOC_T122_SLST_S152}.constraint_name))
- I235 -> zambabpm_RRHH.SLST_S235.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I122; parent_column=I235; ref_schema=zambabpm_RRHH; ref_table=SLST_S235; ref_column=Codigo; constraint_name=FK_DOC_T122_SLST_S235}.constraint_name))
- I238 -> zambabpm_RRHH.SLST_S238.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I122; parent_column=I238; ref_schema=zambabpm_RRHH; ref_table=SLST_S238; ref_column=Codigo; constraint_name=FK_DOC_T122_SLST_S238}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I109 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I235 | numeric(18,0) | YES |  | |
| I162 | datetime | YES |  | |
| I163 | datetime | YES |  | |
| I209 | numeric(18,0) | YES |  | |
| I238 | numeric(18,0) | YES |  | |
| I184 | varchar(200) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I123

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T123.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I123; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T123; ref_column=DOC_ID; constraint_name=FK__DOC_I123__DOC_ID__75E33B6E}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I124

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T124.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I124; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T124; ref_column=DOC_ID; constraint_name=FK__DOC_I124__DOC_ID__033D368C}.constraint_name))
- I152 -> zambabpm_RRHH.SLST_S152.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I124; parent_column=I152; ref_schema=zambabpm_RRHH; ref_table=SLST_S152; ref_column=Codigo; constraint_name=FK_DOC_T124_SLST_S152}.constraint_name))
- I235 -> zambabpm_RRHH.SLST_S235.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I124; parent_column=I235; ref_schema=zambabpm_RRHH; ref_table=SLST_S235; ref_column=Codigo; constraint_name=FK_DOC_T124_SLST_S235}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| I102 | varchar(200) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I152 | numeric(18,0) | YES |  | |
| I235 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I125

**Row count:** 6

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T125.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I125; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T125; ref_column=DOC_ID; constraint_name=FK__DOC_I125__DOC_ID__62F0628B}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I125; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T125_SLST_S118}.constraint_name))
- I160 -> zambabpm_RRHH.SLST_S160.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I125; parent_column=I160; ref_schema=zambabpm_RRHH; ref_table=SLST_S160; ref_column=Codigo; constraint_name=FK_DOC_T125_SLST_S160}.constraint_name))
- I224 -> zambabpm_RRHH.SLST_S224.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I125; parent_column=I224; ref_schema=zambabpm_RRHH; ref_table=SLST_S224; ref_column=Codigo; constraint_name=FK_DOC_T125_SLST_S224}.constraint_name))
- I285 -> zambabpm_RRHH.SLST_S285.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I125; parent_column=I285; ref_schema=zambabpm_RRHH; ref_table=SLST_S285; ref_column=Codigo; constraint_name=FK_DOC_T125_SLST_S285}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I224 | numeric(18,0) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| I259 | numeric(18,0) | YES |  | |
| I290 | numeric(18,0) | YES |  | |
| I169 | numeric(18,0) | YES |  | |
| I285 | numeric(18,0) | YES |  | |
| I293 | datetime | YES |  | |
| I287 | varchar(400) | YES |  | |
| I164 | numeric(18,0) | YES |  | |
| I289 | numeric(18,0) | YES |  | |
| I273 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| I118 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I126

**Row count:** 8

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T126.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I126; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T126; ref_column=DOC_ID; constraint_name=FK__DOC_I126__DOC_ID__0080C572}.constraint_name))
- I185 -> zambabpm_RRHH.SLST_S185.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I126; parent_column=I185; ref_schema=zambabpm_RRHH; ref_table=SLST_S185; ref_column=Codigo; constraint_name=FK_DOC_T126_SLST_S185}.constraint_name))
- I186 -> zambabpm_RRHH.SLST_S186.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I126; parent_column=I186; ref_schema=zambabpm_RRHH; ref_table=SLST_S186; ref_column=Codigo; constraint_name=FK_DOC_T126_SLST_S186}.constraint_name))
- I233 -> zambabpm_RRHH.SLST_S233.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I126; parent_column=I233; ref_schema=zambabpm_RRHH; ref_table=SLST_S233; ref_column=Codigo; constraint_name=FK_DOC_T126_SLST_S233}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I185 | numeric(18,0) | YES |  | |
| I186 | numeric(18,0) | YES |  | |
| I233 | numeric(18,0) | YES |  | |
| I263 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I214 | datetime | YES |  | |
| I264 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I129

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T129.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I129; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T129; ref_column=DOC_ID; constraint_name=FK__DOC_I129__DOC_ID__5CCD74E1}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I269 | numeric(18,0) | YES |  | |
| I271 | datetime | YES |  | |
| I272 | varchar(30) | YES |  | |
| I158 | numeric(18,0) | NO |  | |
| I189 | numeric(18,0) | NO |  | |
| I190 | numeric(18,0) | NO |  | |
| I247 | numeric(18,0) | NO |  | |
| I246 | numeric(18,0) | NO |  | |
| I245 | numeric(18,0) | NO |  | |
| i270 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I130

**Row count:** 1

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T130.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I130; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T130; ref_column=DOC_ID; constraint_name=FK__DOC_I130__DOC_ID__254835CD}.constraint_name))
- I275 -> zambabpm_RRHH.SLST_S275.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I130; parent_column=I275; ref_schema=zambabpm_RRHH; ref_table=SLST_S275; ref_column=Codigo; constraint_name=FK_DOC_T130_SLST_S275}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I275 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I108 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I131

**Row count:** 2

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T131.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I131; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T131; ref_column=DOC_ID; constraint_name=FK__DOC_I131__DOC_ID__3A4352B3}.constraint_name))
- I269 -> zambabpm_RRHH.SLST_S269.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I131; parent_column=I269; ref_schema=zambabpm_RRHH; ref_table=SLST_S269; ref_column=Codigo; constraint_name=FK_DOC_T131_SLST_S269}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I269 | numeric(18,0) | YES |  | |
| I276 | numeric(18,0) | YES |  | |
| I277 | numeric(18,0) | YES |  | |
| I278 | numeric(18,0) | YES |  | |
| I279 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I132

**Row count:** 9

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T132.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T132; ref_column=DOC_ID; constraint_name=FK__DOC_I132__DOC_ID__2236BEF8}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S118}.constraint_name))
- I151 -> zambabpm_RRHH.SLST_S151.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I151; ref_schema=zambabpm_RRHH; ref_table=SLST_S151; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S151}.constraint_name))
- I160 -> zambabpm_RRHH.SLST_S160.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I160; ref_schema=zambabpm_RRHH; ref_table=SLST_S160; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S160}.constraint_name))
- I219 -> zambabpm_RRHH.SLST_S219.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I219; ref_schema=zambabpm_RRHH; ref_table=SLST_S219; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S219}.constraint_name))
- I224 -> zambabpm_RRHH.SLST_S224.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I224; ref_schema=zambabpm_RRHH; ref_table=SLST_S224; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S224}.constraint_name))
- I280 -> zambabpm_RRHH.SLST_S280.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I280; ref_schema=zambabpm_RRHH; ref_table=SLST_S280; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S280}.constraint_name))
- I294 -> zambabpm_RRHH.SLST_S294.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I132; parent_column=I294; ref_schema=zambabpm_RRHH; ref_table=SLST_S294; ref_column=Codigo; constraint_name=FK_DOC_T132_SLST_S294}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I224 | numeric(18,0) | YES |  | |
| I280 | numeric(18,0) | YES |  | |
| I219 | numeric(18,0) | YES |  | |
| I192 | numeric(18,0) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I151 | numeric(18,0) | YES |  | |
| I287 | varchar(400) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| i285 | numeric(7,0) | YES |  | |
| I259 | numeric(18,0) | YES |  | |
| I294 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I134

**Row count:** 9

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T134.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I134; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T134; ref_column=DOC_ID; constraint_name=FK__DOC_I134__DOC_ID__080CD8A1}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I134; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T134_SLST_S118}.constraint_name))
- I160 -> zambabpm_RRHH.SLST_S160.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I134; parent_column=I160; ref_schema=zambabpm_RRHH; ref_table=SLST_S160; ref_column=Codigo; constraint_name=FK_DOC_T134_SLST_S160}.constraint_name))
- I224 -> zambabpm_RRHH.SLST_S224.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I134; parent_column=I224; ref_schema=zambabpm_RRHH; ref_table=SLST_S224; ref_column=Codigo; constraint_name=FK_DOC_T134_SLST_S224}.constraint_name))
- I285 -> zambabpm_RRHH.SLST_S285.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I134; parent_column=I285; ref_schema=zambabpm_RRHH; ref_table=SLST_S285; ref_column=Codigo; constraint_name=FK_DOC_T134_SLST_S285}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I169 | numeric(18,0) | YES |  | |
| I259 | numeric(18,0) | YES |  | |
| I261 | numeric(18,0) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| I224 | numeric(18,0) | YES |  | |
| I289 | numeric(18,0) | YES |  | |
| I293 | datetime | YES |  | |
| I285 | numeric(18,0) | YES |  | |
| I287 | varchar(400) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I164 | numeric(18,0) | YES |  | |
| i258 | numeric(18,0) | YES |  | |
| I295 | numeric(18,0) | YES |  | |
| I290 | numeric(18,0) | YES |  | |
| I118 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I135

**Row count:** 5

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T135.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T135; ref_column=DOC_ID; constraint_name=FK__DOC_I135__DOC_ID__49DA9BFE}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S118}.constraint_name))
- I151 -> zambabpm_RRHH.SLST_S151.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I151; ref_schema=zambabpm_RRHH; ref_table=SLST_S151; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S151}.constraint_name))
- I160 -> zambabpm_RRHH.SLST_S160.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I160; ref_schema=zambabpm_RRHH; ref_table=SLST_S160; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S160}.constraint_name))
- I219 -> zambabpm_RRHH.SLST_S219.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I219; ref_schema=zambabpm_RRHH; ref_table=SLST_S219; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S219}.constraint_name))
- I224 -> zambabpm_RRHH.SLST_S224.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I224; ref_schema=zambabpm_RRHH; ref_table=SLST_S224; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S224}.constraint_name))
- I280 -> zambabpm_RRHH.SLST_S280.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I135; parent_column=I280; ref_schema=zambabpm_RRHH; ref_table=SLST_S280; ref_column=Codigo; constraint_name=FK_DOC_T135_SLST_S280}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I219 | numeric(18,0) | YES |  | |
| I160 | numeric(18,0) | YES |  | |
| I224 | numeric(18,0) | YES |  | |
| I280 | numeric(18,0) | YES |  | |
| I192 | numeric(18,0) | YES |  | |
| I166 | numeric(18,0) | YES |  | |
| I259 | numeric(18,0) | YES |  | |
| I261 | numeric(18,0) | YES |  | |
| I169 | numeric(18,0) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I151 | numeric(18,0) | YES |  | |
| i285 | numeric(7,0) | YES |  | |
| I287 | varchar(400) | YES |  | |

---

## zambabpm_RRHH.DOC_I136

**Row count:** 3

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T136.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I136; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T136; ref_column=DOC_ID; constraint_name=FK__DOC_I136__DOC_ID__69DD573C}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I264 | numeric(18,0) | YES |  | |
| I258 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I296 | numeric(18,0) | YES |  | |
| I110 | varchar(30) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I102 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_I137

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T137.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I137; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T137; ref_column=DOC_ID; constraint_name=FK__DOC_I137__DOC_ID__0E1AB7B2}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I138

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T138.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I138; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T138; ref_column=DOC_ID; constraint_name=FK__DOC_I138__DOC_ID__11813442}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I_309 | varchar(2000) | YES |  | |
| I_310 | varchar(2000) | YES |  | |
| I_311 | varchar(2000) | YES |  | |
| I_312 | varchar(2000) | YES |  | |

---

## zambabpm_RRHH.DOC_I139

**Row count:** 784

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T139.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I139; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T139; ref_column=DOC_ID; constraint_name=FK__DOC_I139__DOC_ID__656DA7DA}.constraint_name))
- I204 -> zambabpm_RRHH.SLST_S204.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I139; parent_column=I204; ref_schema=zambabpm_RRHH; ref_table=SLST_S204; ref_column=Codigo; constraint_name=FK_DOC_T139_SLST_S204}.constraint_name))
- I317 -> zambabpm_RRHH.SLST_S317.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I139; parent_column=I317; ref_schema=zambabpm_RRHH; ref_table=SLST_S317; ref_column=Codigo; constraint_name=FK_DOC_T139_SLST_S317}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| i313 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I177 | varchar(200) | YES |  | |
| I317 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I318 | varchar(200) | YES |  | |
| i314 | numeric(18,0) | YES |  | |
| i316 | numeric(18,0) | YES |  | |
| I204 | numeric(18,0) | YES |  | |
| I250 | varchar(200) | YES |  | |
| I257 | numeric(18,0) | YES |  | |
| I237 | varchar(30) | YES |  | |

---

## zambabpm_RRHH.DOC_I140

**Row count:** 13

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T140.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I140; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T140; ref_column=DOC_ID; constraint_name=FK__DOC_I140__DOC_ID__142896C3}.constraint_name))
- I320 -> zambabpm_RRHH.SLST_S320.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I140; parent_column=I320; ref_schema=zambabpm_RRHH; ref_table=SLST_S320; ref_column=Codigo; constraint_name=FK_DOC_T140_SLST_S320}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I313 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I320 | numeric(18,0) | YES |  | |
| I327 | datetime | YES |  | |

---

## zambabpm_RRHH.DOC_I142

**Row count:** 10

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T142.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I142; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T142; ref_column=DOC_ID; constraint_name=FK__DOC_I142__DOC_ID__43D7A9E5}.constraint_name))
- I118 -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I142; parent_column=I118; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=FK_DOC_T142_SLST_S118}.constraint_name))
- I119 -> zambabpm_RRHH.SLST_S119.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I142; parent_column=I119; ref_schema=zambabpm_RRHH; ref_table=SLST_S119; ref_column=Codigo; constraint_name=FK_DOC_T142_SLST_S119}.constraint_name))
- I121 -> zambabpm_RRHH.SLST_S121.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I142; parent_column=I121; ref_schema=zambabpm_RRHH; ref_table=SLST_S121; ref_column=Codigo; constraint_name=FK_DOC_T142_SLST_S121}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I313 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I172 | varchar(200) | YES |  | |
| I118 | numeric(18,0) | YES |  | |
| I119 | numeric(18,0) | YES |  | |
| I121 | numeric(18,0) | YES |  | |
| I114 | varchar(200) | YES |  | |
| I115 | numeric(18,0) | YES |  | |
| I116 | varchar(30) | YES |  | |
| I184 | varchar(200) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I319 | numeric(18,0) | YES |  | |
| I124 | varchar(30) | YES |  | |

---

## zambabpm_RRHH.DOC_I143

**Row count:** 8

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T143.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I143; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T143; ref_column=DOC_ID; constraint_name=FK__DOC_I143__DOC_ID__7386BD07}.constraint_name))
- I320 -> zambabpm_RRHH.SLST_S320.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I143; parent_column=I320; ref_schema=zambabpm_RRHH; ref_table=SLST_S320; ref_column=Codigo; constraint_name=FK_DOC_T143_SLST_S320}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I323 | numeric(18,0) | YES |  | |
| I313 | numeric(18,0) | YES |  | |
| I109 | numeric(18,0) | YES |  | |
| I101 | varchar(200) | YES |  | |
| I321 | numeric(18,0) | YES |  | |
| I136 | varchar(200) | YES |  | |
| I325 | numeric(18,0) | YES |  | |
| I260 | numeric(18,0) | NO |  | |
| I172 | varchar(200) | YES |  | |
| I320 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I144

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T144.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I144; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T144; ref_column=DOC_ID; constraint_name=FK__DOC_I144__DOC_ID__5D6271BE}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |

---

## zambabpm_RRHH.DOC_I145

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T145.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I145; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T145; ref_column=DOC_ID; constraint_name=FK__DOC_I145__DOC_ID__69C848A3}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |

---

## zambabpm_RRHH.DOC_I146

**Row count:** 35

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T146.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I146; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T146; ref_column=DOC_ID; constraint_name=FK__DOC_I146__DOC_ID__762E1F88}.constraint_name))
- I353 -> zambabpm_RRHH.SLST_S353.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I146; parent_column=I353; ref_schema=zambabpm_RRHH; ref_table=SLST_S353; ref_column=Codigo; constraint_name=FK_DOC_T146_SLST_S353}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I331 | varchar(200) | YES |  | |
| I328 | varchar(200) | YES |  | |
| I271 | datetime | YES |  | |
| I329 | varchar(4000) | YES |  | |
| I330 | varchar(500) | YES |  | |
| I249 | numeric(18,0) | YES |  | |
| I339 | varchar(200) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| i270 | varchar(200) | YES |  | |
| i338 | varchar(200) | YES |  | |
| I340 | numeric(18,0) | YES |  | |
| I334 | datetime | YES |  | |
| I335 | datetime | YES |  | |
| I352 | numeric(18,0) | YES |  | |
| I353 | numeric(18,0) | YES |  | |
| I349 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I148

**Row count:** 8

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T148.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I148; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T148; ref_column=DOC_ID; constraint_name=FK__DOC_I148__DOC_ID__2AA1E7C7}.constraint_name))
- I330 -> zambabpm_RRHH.SLST_S330.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I148; parent_column=I330; ref_schema=zambabpm_RRHH; ref_table=SLST_S330; ref_column=Codigo; constraint_name=FK_DOC_T148_SLST_S330}.constraint_name))
- I331 -> zambabpm_RRHH.SLST_S331.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I148; parent_column=I331; ref_schema=zambabpm_RRHH; ref_table=SLST_S331; ref_column=Codigo; constraint_name=FK_DOC_T148_SLST_S331}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I329 | varchar(4000) | YES |  | |
| I339 | varchar(200) | YES |  | |
| I330 | varchar(500) | YES |  | |
| I331 | varchar(200) | YES |  | |
| I328 | varchar(200) | YES |  | |
| I136 | varchar(200) | YES |  | |
| i338 | varchar(200) | YES |  | |
| I340 | numeric(18,0) | YES |  | |
| I271 | datetime | YES |  | |
| I270 | varchar(200) | YES |  | |
| I352 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I149

**Row count:** 4

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T149.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I149; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T149; ref_column=DOC_ID; constraint_name=FK__DOC_I149__DOC_ID__016AC80A}.constraint_name))
- I330 -> zambabpm_RRHH.SLST_S330.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I149; parent_column=I330; ref_schema=zambabpm_RRHH; ref_table=SLST_S330; ref_column=Codigo; constraint_name=FK_DOC_T149_SLST_S330}.constraint_name))
- I331 -> zambabpm_RRHH.SLST_S331.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I149; parent_column=I331; ref_schema=zambabpm_RRHH; ref_table=SLST_S331; ref_column=Codigo; constraint_name=FK_DOC_T149_SLST_S331}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I286 | datetime | YES |  | |
| I341 | numeric(18,0) | YES |  | |
| I329 | varchar(4000) | YES |  | |
| I331 | varchar(200) | YES |  | |
| I330 | varchar(500) | YES |  | |
| I138 | varchar(200) | YES |  | |
| I270 | varchar(200) | YES |  | |
| I326 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I150

**Row count:** 290

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T150.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I150; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T150; ref_column=DOC_ID; constraint_name=FK__DOC_I150__DOC_ID__2E3D6E81}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I343 | varchar(200) | YES |  | |
| I344 | varchar(200) | YES |  | |
| I347 | numeric(18,0) | YES |  | |
| I348 | numeric(18,0) | YES |  | |
| i342 | datetime | YES |  | |
| I341 | numeric(18,0) | YES |  | |
| I258 | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_I151

**Row count:** 16

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T151.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I151; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T151; ref_column=DOC_ID; constraint_name=FK__DOC_I151__DOC_ID__0D9B94C5}.constraint_name))
- I344 -> zambabpm_RRHH.SLST_S344.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I151; parent_column=I344; ref_schema=zambabpm_RRHH; ref_table=SLST_S344; ref_column=Codigo; constraint_name=FK_DOC_T151_SLST_S344}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I341 | numeric(18,0) | YES |  | |
| I326 | numeric(18,0) | YES |  | |
| I349 | numeric(18,0) | YES |  | |
| I344 | varchar(200) | YES |  | |
| I136 | varchar(4000) | YES |  | |
| I346 | varchar(200) | YES |  | |
| I351 | varchar(200) | YES |  | |
| I354 | datetime | YES |  | |
| I342 | datetime | YES |  | |

---

## zambabpm_RRHH.DOC_I152

**Row count:** 4

**Description:** _TODO_

**Foreign keys:**
- DOC_ID -> zambabpm_RRHH.DOC_T152.DOC_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I152; parent_column=DOC_ID; ref_schema=zambabpm_RRHH; ref_table=DOC_T152; ref_column=DOC_ID; constraint_name=FK__DOC_I152__DOC_ID__2FF0ACC9}.constraint_name))
- I330 -> zambabpm_RRHH.SLST_S330.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_I152; parent_column=I330; ref_schema=zambabpm_RRHH; ref_table=SLST_S330; ref_column=Codigo; constraint_name=FK_DOC_T152_SLST_S330}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| lupdate | datetime | YES |  | |
| crdate | datetime | YES |  | |
| Fecha | smalldatetime | YES |  | |
| DOC_ID | numeric(18,0) | NO | PK | |
| I249 | numeric(18,0) | YES |  | |
| I330 | varchar(500) | YES |  | |
| I352 | numeric(18,0) | YES |  | |
| i355 | varchar(200) | YES |  | |

---

## zambabpm_RRHH.DOC_T101

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T102

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T103

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T104

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T105

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T107

**Row count:** 33

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T108

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T109

**Row count:** 17

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T110

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T111

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T113

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T114

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T118

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T119

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T120

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T121

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T122

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T123

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T124

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T125

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T126

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T129

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T130

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T131

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T132

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T134

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T135

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T136

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T137

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T138

**Row count:** 0

**Description:** _TODO_

**Foreign keys:**
- VOL_ID -> dbo.DISK_VOLUME.DISK_VOL_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_T138; parent_column=VOL_ID; ref_schema=dbo; ref_table=DISK_VOLUME; ref_column=DISK_VOL_ID; constraint_name=FK__DOC_T138__VOL_ID__0CBC7F25}.constraint_name))
- VOL_ID -> dbo.DISK_VOLUME.DISK_VOL_ID ($(@{parent_schema=zambabpm_RRHH; parent_table=DOC_T138; parent_column=VOL_ID; ref_schema=dbo; ref_table=DISK_VOLUME; ref_column=DISK_VOL_ID; constraint_name=FK_DOC_T138_DISK_VOLUME}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T139

**Row count:** 784

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T140

**Row count:** 13

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T142

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T143

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T144

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T145

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T146

**Row count:** 35

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nvarchar(-1) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T148

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T149

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T150

**Row count:** 290

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T151

**Row count:** 16

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DOC_T152

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | NO | PK | |
| FOLDER_ID | numeric(18,0) | YES |  | |
| DISK_GROUP_ID | int | YES |  | |
| PLATTER_ID | int | YES |  | |
| VOL_ID | int | YES |  | |
| DOC_FILE | nchar(200) | YES |  | |
| OFFSET | int | YES |  | |
| DOC_TYPE_ID | int | YES |  | |
| NAME | nvarchar(255) | YES |  | |
| ICON_ID | int | YES |  | |
| SHARED | int | YES |  | |
| Original_filename | nvarchar(255) | YES |  | |
| ver_Parent_id | numeric(18,0) | YES |  | |
| version | int | YES |  | |
| RootId | numeric(18,0) | YES |  | |
| NumeroVersion | int | YES |  | |
| FileSize | decimal(18,0) | YES |  | |

---

## zambabpm_RRHH.DocumentsFromPath

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_Type_ID | bigint | YES |  | |
| Path | varchar(500) | YES |  | |
| Status | varchar(50) | YES |  | |
| Last_Update | datetime | YES |  | |

---

## zambabpm_RRHH.DocumentsFromPathLevels

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Doc_Type_ID | bigint | YES |  | |
| Level | int | YES |  | |
| Doc_Index | bigint | YES |  | |

---

## zambabpm_RRHH.EncriptedFiles

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| DOC_ID | numeric(18,0) | YES |  | |
| DOC_TYPE_ID | numeric(18,0) | YES |  | |

---

## zambabpm_RRHH.EventLogDetails

**Row count:** 20

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | bigint | YES |  | |
| Name | varchar(200) | YES |  | |
| Description | varchar(-1) | YES |  | |
| EventId | bigint | YES |  | |

---

## zambabpm_RRHH.ILST_I344

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ITEMID | int | NO | PK | |
| ITEM | nvarchar(200) | NO |  | |

---

## zambabpm_RRHH.Paises

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | int | NO | PK | |
| name | varchar(1) | NO |  | |
| iso_name | varchar(1) | NO |  | |
| alfa2 | varchar(1) | NO |  | |
| alfa3 | varchar(1) | NO |  | |
| numerico | int | NO |  | |

---

## zambabpm_RRHH.pendingverificationusers

**Row count:** 44

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | int | NO | PK | |
| userid | int | NO |  | |
| token | varchar(-1) | NO |  | |

---

## zambabpm_RRHH.ReportCategories

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| Category | varchar(255) | NO |  | |

---

## zambabpm_RRHH.ReportChartConfig

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ChartConfigId | int | NO |  | |
| ReportId | int | NO |  | |

---

## zambabpm_RRHH.ReportChartContainer

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ReportID | int | NO |  | |
| ChartContainerID | int | NO |  | |

---

## zambabpm_RRHH.reportDescription

**Row count:** 43

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| ReportId | int | NO |  | |
| Description | varchar(255) | YES |  | |

---

## zambabpm_RRHH.ReportDescription_Backup13032026

**Row count:** 47

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO |  | |
| ReportId | int | NO |  | |
| Description | text(2147483647) | YES |  | |

---

## zambabpm_RRHH.reporteGeneral_ReportCategories

**Row count:** 44

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ReporteGeneralId | int | YES |  | |
| ReporteCategoryId | int | YES |  | |

---

## zambabpm_RRHH.reporteGeneral_reportDescription

**Row count:** 44

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| reporteGeneral_Id | int | YES |  | |
| reportDescription_Id | int | YES |  | |

---

## zambabpm_RRHH.resetpasswordtokens

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | int | NO | PK | |
| email | varchar(100) | NO |  | |
| tokendata | varchar(50) | NO |  | |
| expirationdate | datetime | NO |  | |
| used | bit | NO |  | |

---

## zambabpm_RRHH.RH_SidebarMenuItem

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| menuitem_id | int | NO | PK | |
| name | varchar(-1) | NO |  | |
| title | varchar(-1) | NO |  | |
| description | varchar(-1) | YES |  | |
| action | varchar(-1) | YES |  | |
| parameters | varchar(-1) | YES |  | |
| position | int | NO |  | |
| icon | varchar(-1) | YES |  | |

---

## zambabpm_RRHH.Rol

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| rol_id | int | NO | PK | |
| rolname | nvarchar(-1) | NO |  | |

---

## zambabpm_RRHH.SecurityReportLastGenerated

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| last_date_report | datetime | YES |  | |

---

## zambabpm_RRHH.SLST_S103

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S104

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S105

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S106

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S118

**Row count:** 240

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S119

**Row count:** 24

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S121

**Row count:** 41

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S125

**Row count:** 241

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(200) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S126

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S134

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S135

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S137

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S139

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S143

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S144

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S150

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S151

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S152

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S153

**Row count:** 13

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S157

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S159

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S160

**Row count:** 31

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S165

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S178

**Row count:** 14

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S182

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S185

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S186

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S187

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S193

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S194

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S197

**Row count:** 16

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S198

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S199

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S204

**Row count:** 27

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(200) | YES |  | |

---

## zambabpm_RRHH.SLST_S207

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S216

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S217

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S218

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S219

**Row count:** 22

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S220

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S221

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S223

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S224

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S232

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S233

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S234

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S235

**Row count:** 15

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S238

**Row count:** 6

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S240

**Row count:** 4

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S241

**Row count:** 23

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S269

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S275

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S280

**Row count:** 8

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S285

**Row count:** 13

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S291

**Row count:** 12

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S294

**Row count:** 3

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S296

**Row count:** 13

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S314

**Row count:** 7

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S316

**Row count:** 2

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S317

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S320

**Row count:** 20

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S321

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S330

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(500) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S331

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(200) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S344

**Row count:** 10

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(200) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S346

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | varchar(200) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S349

**Row count:** 21

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.SLST_S353

**Row count:** 5

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Codigo | numeric(18,0) | NO | PK | |
| Descripcion | varchar(60) | YES |  | |

---

## zambabpm_RRHH.UserEventLogs

**Row count:** 6303

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Tracking Number | bigint | YES |  | |
| Application Component | varchar(500) | YES |  | |
| Context | varchar(500) | YES |  | |
| Actor Name | varchar(500) | YES |  | |
| Group | varchar(500) | YES |  | |
| responsecode | varchar(500) | YES |  | |
| Event name | varchar(500) | YES |  | |
| Event Type | varchar(50) | YES |  | |
| Msg | varchar(500) | YES |  | |
| dname | varchar(500) | YES |  | |
| Client IP Address | varchar(500) | YES |  | |
| policy | varchar(500) | YES |  | |
| sessiontype | varchar(500) | YES |  | |
| Timestamp | datetime | YES |  | |
| Event Code | bigint | YES |  | |
| Severity | varchar(20) | YES |  | |
| SIEMAPP | varchar(50) | YES |  | |
| Location | varchar(50) | YES |  | |
| Application Code | varchar(200) | YES |  | |
| Identity Provider | varchar(50) | YES |  | |
| Application Instance | varchar(200) | YES |  | |
| Acting As User | varchar(50) | YES |  | |
| Authentication Channel | varchar(500) | YES |  | |
| Session ID | varchar(500) | YES |  | |
| Correlation ID | varchar(50) | YES |  | |

---

## zambabpm_RRHH.usr_rightsBKP

**Row count:** 10456

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| GROUPID | numeric(10,0) | NO |  | |
| OBJID | numeric(10,0) | NO |  | |
| RTYPE | numeric(10,0) | NO |  | |
| ADITIONAL | numeric(10,0) | NO |  | |

---

## zambabpm_RRHH.VideoplayerURLSources

**Row count:** 1

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| videoplayersourceid | int | NO | PK | |
| userid | int | YES |  | |
| youtubevideoid | varchar(-1) | YES |  | |

---

## zambabpm_RRHH.VideoWidgets

**Row count:** 0

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| videoid | nvarchar(-1) | NO |  | |
| url | nchar(10) | YES |  | |
| groupid | int | NO |  | |

---

## zambabpm_RRHH.WidgetsContainer

**Row count:** 9

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | int | NO | PK | |
| UserId | int | NO |  | |
| Options | varchar(500) | NO |  | |
| WidgetCoordinates | varchar(1000) | YES |  | |

---

## zambabpm_RRHH.ZDoSearchResults

**Row count:** 25

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | bigint | NO |  | |
| UserId | bigint | NO |  | |
| Mode | varchar(50) | YES |  | |
| ExpirationDate | datetime | NO |  | |
| SearchObject | varbinary(-1) | YES |  | |

---

## zambabpm_RRHH.ZHierarchy_I118_I119

**Row count:** 24

**Description:** _TODO_

**Foreign keys:**
- ParentValue -> zambabpm_RRHH.SLST_S118.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=ZHierarchy_I118_I119; parent_column=ParentValue; ref_schema=zambabpm_RRHH; ref_table=SLST_S118; ref_column=Codigo; constraint_name=HierarchicalFK_772797934}.constraint_name))
- ChildValue -> zambabpm_RRHH.SLST_S119.Codigo ($(@{parent_schema=zambabpm_RRHH; parent_table=ZHierarchy_I118_I119; parent_column=ChildValue; ref_schema=zambabpm_RRHH; ref_table=SLST_S119; ref_column=Codigo; constraint_name=HierarchicalFK_971441277}.constraint_name))

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| ParentValue | numeric(18,0) | NO | PK | |
| ChildValue | numeric(18,0) | NO | PK | |

---

## zambabpm_RRHH.ZLSR

**Row count:** 437

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| id | bigint | NO |  | |
| UserId | bigint | NO |  | |
| Mode | varchar(50) | NO |  | |
| SearchDate | datetime | NO |  | |
| SearchObject | varbinary(-1) | NO |  | |
| Name | varchar(4000) | NO |  | |

---

## zambabpm_RRHH.ZWebBookmarks

**Row count:** 11

**Description:** _TODO_

| Column | Type | Nullable | PK | Description |
|--------|------|----------|----|--------------|
| Id | bigint | NO | PK | |
| DocId | bigint | YES |  | |
| DocTypeId | bigint | YES |  | |
| TaskId | bigint | YES |  | |
| ReplaceTaskDocument | bit | YES |  | |
| Title | nvarchar(500) | YES |  | |
| Description | nvarchar(-1) | YES |  | |
| Image | nvarchar(-1) | YES |  | |
| Url | nvarchar(-1) | YES |  | |
| SiteName | nvarchar(500) | YES |  | |
| FileType | nvarchar(100) | YES |  | |
| Logo | nvarchar(-1) | YES |  | |
| ExtraMetadata | nvarchar(-1) | YES |  | |

---


