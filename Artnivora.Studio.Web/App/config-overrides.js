var path = require ('path');
var fs = require ('fs');
const {
    override,
    addDecoratorsLegacy,
    babelInclude,
    disableEsLint,
} = require("customize-cra");

module.exports = function (config, env) {
    return Object.assign(config, override(
        disableEsLint(),
        addDecoratorsLegacy(),
        /*Make sure Babel compiles the stuff in the common folder*/
        babelInclude([
            path.resolve('src'), // don't forget this
            fs.realpathSync("node_modules/hvb-shared-frontend/src")
        ])
        )(config, env)
    )
}