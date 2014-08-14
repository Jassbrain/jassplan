var Jassplan = Jassplan || {};

Jassplan.NoteModel = function(config) {
    this.id = config.id;
    this.dateCreated = config.dateCreated;
    this.title = config.title;
    this.narrative = config.narrative;
    this.description = config.description;
    this.estimatedDuration = config.estimatedDuration;
    this.doneDate = config.doneDate;
    this.actualDuration = config.actualDuration;
};

Jassplan.NoteModel.prototype.isValid = function () {
    { return true; }
    return false; };
