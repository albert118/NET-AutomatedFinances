import React from 'react';


export default class DateField extends React.Component {
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
                    htmlFor={`date-input-${this.uuid}`}
                    className="form-label--font form-value--label"
                    id={`date-label-${this.uuid}`}
                    name={this.props.labelName}
                    value={this.props.labelName}
                >
                    { this.inputLabelText }
                </label>
                <input 
                    className="form-value--font no-padding form-input form-small-date-entry-value"
                    id={`text-input-${this.uuid}`}
                    type="date"
                    name="date-input"
                    title={this.props.inputLabelText}
                    value={this.state.value}
                    placeholder={this.props.prompt}
                    onChange={this.handleChange}
                />
            </div>
        );
    }
};