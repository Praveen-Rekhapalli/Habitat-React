class FaqItem extends React.Component 
{
    render() {
        return (
		<div className="panel panel-default">
			<div className="panel-heading" role="tab">
				<div className="panel-title">
					<a role="button" className="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href={'#faq' + this.props.data.Id}>
						<span className="glyphicon glyphicon-search" aria-hidden="true"></span>
						<span dangerouslySetInnerHTML={{ __html: this.props.data.Question }} />
					</a>
				</div>
			</div>
			<div id={'faq' + this.props.data.Id} className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingcollapse">
				<div className="panel-body" dangerouslySetInnerHTML={{ __html: this.props.data.Answer }} />
			</div>
		</div>
		);
					}
}

class FaqAccordionReact extends React.Component 
{
    render() {
        return (
			<div className="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
			    {this.props.data.Items.map(function (faq) {
			        return <FaqItem key={faq.Id} data={faq } />;
			    })}
			</div>
		);
    }
}

