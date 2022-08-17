import React from 'react';


export default class CurrencyField extends React.Component {
    // TODO: set a maximum date of today (future dating transactions should not be valid)
    constructor(props) {
        super(props);

        this.props = props;

        this.state = { value: '' };

        this.uuid = Math.random();
        this.inputLabelText = props.label ? props.label : 'Sample Label'

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        this.setState({value: event.target.value});
    }

    render() {
        return(
            <div className={`form-entry-card ${ this.props.className }`}>
                <label
                    htmlFor={`monetary-input-${this.uuid}`}
                    className="form-label--font form-value--label"
                    id={`monetary-label-${this.uuid}`}
                    name={this.props.labelName}
                    value={this.props.labelName}
                >
                    { this.inputLabelText }
                </label>
                <input 
                    className="form-value--font no-padding form-input form-small-monetary-entry-value"
                    id={`text-input-${this.uuid}`}
                    type="currency"
                    name="monetary-input"
                    title={this.props.inputLabelText}
                    value={this.state.value}
                    placeholder={this.props.prompt}
                    onChange={this.handleChange}
                />
            </div>
        );
    }
};