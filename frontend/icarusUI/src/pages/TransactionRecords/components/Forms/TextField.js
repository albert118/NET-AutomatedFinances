import React from 'react';


export default class TextField extends React.Component {
    constructor(props) {
        super(props);

        this.props = props;
        this.state = { value: props.prompt };

        this.uuid = `text-input-${Math.random()}`;
        this.inputLabelText = props.label ? props.label : 'Sample Label'

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        this.setState({value: event.target.value});
    }

    render() {
        return(
            <div className={`form-entry ${ this.props.className }`}>
                <label
                    for={this.uuid}
                    className="form-label--font form-value--label"
                    id={this.uuid}
                    name={this.props.labelName}
                    value={this.props.labelName}
                >
                    { this.inputLabelText }
                </label>
                <input 
                    className="form-value--font no-padding form-small-text-entry-value"
                    id={this.uuid}
                    type="text"
                    name="text-input"
                    title={this.props.inputLabelText}
                    value={this.state.value}
                    onChange={this.handleChange}
                />
            </div>
        );
    }
};