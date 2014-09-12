var Jassplan = Jassplan || {};

Jassplan.NoteModel = function(config) {
    this.id = config.id;
    this.jassActivityID = config.jassActivityID;
    this.dateCreated = config.dateCreated;
    this.title = config.title;
    this.narrative = config.narrative;
    this.description = config.description;
    this.estimatedDuration = config.estimatedDuration;
    this.estimatedStartHour = config.estimatedStartHour;
    this.doneDate = config.doneDate;
    this.actualDuration = config.actualDuration;
};

Jassplan.NoteModel.prototype.isValid = function () {
    { return true; }
    return false; };
