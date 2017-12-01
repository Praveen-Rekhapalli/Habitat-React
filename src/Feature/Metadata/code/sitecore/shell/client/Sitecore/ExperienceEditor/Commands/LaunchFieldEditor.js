define(["sitecore","/-/speak/v1/ExperienceEditor/ExperienceEditor.js"],function(Sitecore,ExperienceEditor){
    Sitecore.Commands.LaunchFieldEditor =
	{
		canExecute:function(context)
		{
			return true;
		},
		execute:function(context)
		{
            context.currentContext.argument = context.button.viewModel.$el[0].accessKey;			
            ExperienceEditor.PipelinesUtil.generateRequestProcessor("ExperienceEditor.GenerateFieldEditorUrl", function(response)
            {
             var dialogUrl = response.responseValue.value;
             var dialogFeatures = "dialogHeight: 380px;dialogWidth: 640px;edge:raised; center:yes; help:no;resizable:yes;status:no;scroll:no"; 
             ExperienceEditor.Dialogs.showModalDialog(dialogUrl,'',dialogFeatures,null);
            }).execute(context);
		}
	};
	
});