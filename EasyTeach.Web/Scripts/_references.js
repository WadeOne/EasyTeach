﻿/// <reference path="jquery-2.1.1.js" />
/// <autosync enabled="true" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery-ui-1.9.2.js" />
/// <reference path="knockout-2.2.0.js" />
/// <reference path="modernizr-2.6.2.js" />
/// <reference path="respond.js" />
/// <reference path="webapitestclient.js" />
/// <reference path="app/app.js" />
/// <reference path="app/application.js" />
/// <reference path="app/fileupload.js" />
/// <reference path="app/routes.js" />
/// <reference path="app/settings.js" />
/// <reference path="app/config/menu.js" />
/// <reference path="app/config/public-routes.js" />
/// <reference path="app/controllers/error-controller.js" />
/// <reference path="app/controllers/home-controller.js" />
/// <reference path="app/controllers/students-controller.js" />
/// <reference path="app/lib/utils.js" />
/// <reference path="app/lib/view-helper.js" />
/// <reference path="app/models/confirm-email.js" />
/// <reference path="app/models/hello-world.js" />
/// <reference path="app/models/reset-password.js" />
/// <reference path="app/models/set-password.js" />
/// <reference path="app/models/user-login.js" />
/// <reference path="app/models/user-logout.js" />
/// <reference path="app/models/user.js" />
/// <reference path="app/utils/serialize.js" />
/// <reference path="app/views/add-homeworks-view.js" />
/// <reference path="app/views/confirm-email-view.js" />
/// <reference path="app/views/hello-world-view.js" />
/// <reference path="app/views/login-view.js" />
/// <reference path="app/views/logout-view.js" />
/// <reference path="app/views/register-view.js" />
/// <reference path="app/views/reset-password-view.js" />
/// <reference path="app/views/set-password.js" />
/// <reference path="app/views/site-view.js" />
/// <reference path="app/views/student-homeworks-view.js" />
/// <reference path="app/controllers/base/controller.js" />
/// <reference path="app/controllers/quizzes/quiz-controller.js" />
/// <reference path="app/models/base/collection.js" />
/// <reference path="app/models/base/model.js" />
/// <reference path="app/models/errors/error.js" />
/// <reference path="app/models/quizzes/question.js" />
/// <reference path="app/models/quizzes/quiz-list.js" />
/// <reference path="app/models/quizzes/quiz-short.js" />
/// <reference path="app/models/shared/menu-item.js" />
/// <reference path="app/models/shared/menu.js" />
/// <reference path="app/models/user/session.js" />
/// <reference path="app/views/base/collection-view.js" />
/// <reference path="app/views/base/view.js" />
/// <reference path="app/views/errors/error.js" />
/// <reference path="app/views/quizzes/add-quiz-view.js" />
/// <reference path="app/views/quizzes/answer-text-view.js" />
/// <reference path="app/views/quizzes/edit-quiz-view.js" />
/// <reference path="app/views/quizzes/question-list-view.js" />
/// <reference path="app/views/quizzes/question-view.js" />
/// <reference path="app/views/quizzes/quiz-description-view.js" />
/// <reference path="app/views/quizzes/quiz-list-view.js" />
/// <reference path="app/views/quizzes/quiz-view.js" />
/// <reference path="app/views/shared/menu.js" />
/// <reference path="app/views/students/grades-view.js" />
/// <reference path="vendor/bower_components/backbone/backbone.js" />
/// <reference path="vendor/bower_components/backbone/index.js" />
/// <reference path="vendor/bower_components/chaplin/chaplin.js" />
/// <reference path="vendor/bower_components/handlebars/handlebars.amd.js" />
/// <reference path="vendor/bower_components/handlebars/handlebars.js" />
/// <reference path="vendor/bower_components/handlebars/handlebars.runtime.amd.js" />
/// <reference path="vendor/bower_components/handlebars/handlebars.runtime.js" />
/// <reference path="vendor/bower_components/jquery-placeholder/jquery.placeholder.js" />
/// <reference path="vendor/bower_components/jquery.cookie/jquery.cookie.js" />
/// <reference path="vendor/bower_components/modernizr/grunt.js" />
/// <reference path="vendor/bower_components/modernizr/modernizr.js" />
/// <reference path="vendor/bower_components/requirejs/require.js" />
/// <reference path="vendor/bower_components/requirejs-text/text.js" />
/// <reference path="vendor/bower_components/underscore/underscore.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation.min.js" />
/// <reference path="vendor/bower_components/fastclick/lib/fastclick.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation.js" />
/// <reference path="vendor/bower_components/jquery/dist/jquery.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes.js" />
/// <reference path="vendor/bower_components/jquery/src/callbacks.js" />
/// <reference path="vendor/bower_components/jquery/src/core.js" />
/// <reference path="vendor/bower_components/jquery/src/css.js" />
/// <reference path="vendor/bower_components/jquery/src/data.js" />
/// <reference path="vendor/bower_components/jquery/src/deferred.js" />
/// <reference path="vendor/bower_components/jquery/src/deprecated.js" />
/// <reference path="vendor/bower_components/jquery/src/dimensions.js" />
/// <reference path="vendor/bower_components/jquery/src/effects.js" />
/// <reference path="vendor/bower_components/jquery/src/event.js" />
/// <reference path="vendor/bower_components/jquery/src/intro.js" />
/// <reference path="vendor/bower_components/jquery/src/jquery.js" />
/// <reference path="vendor/bower_components/jquery/src/manipulation.js" />
/// <reference path="vendor/bower_components/jquery/src/offset.js" />
/// <reference path="vendor/bower_components/jquery/src/outro.js" />
/// <reference path="vendor/bower_components/jquery/src/queue.js" />
/// <reference path="vendor/bower_components/jquery/src/selector-native.js" />
/// <reference path="vendor/bower_components/jquery/src/selector-sizzle.js" />
/// <reference path="vendor/bower_components/jquery/src/selector.js" />
/// <reference path="vendor/bower_components/jquery/src/serialize.js" />
/// <reference path="vendor/bower_components/jquery/src/traversing.js" />
/// <reference path="vendor/bower_components/jquery/src/wrap.js" />
/// <reference path="vendor/bower_components/lodash/dist/lodash.compat.js" />
/// <reference path="vendor/bower_components/lodash/dist/lodash.js" />
/// <reference path="vendor/bower_components/lodash/dist/lodash.underscore.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/a-download.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/audio-audiodata-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/audio-webaudio-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/battery-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/battery-level.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/blob-constructor.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/canvas-todataurl-type.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/contenteditable.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/contentsecuritypolicy.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/contextmenu.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/cookies.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/cors.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-backgroundposition-shorthand.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-backgroundposition-xy.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-backgroundrepeat.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-backgroundsizecover.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-boxsizing.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-calc.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-cubicbezierrange.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-displayrunin.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-displaytable.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-filters.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-hyphens.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-lastchild.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-mask.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-mediaqueries.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-objectfit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-overflow-scrolling.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-pointerevents.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-positionsticky.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-regions.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-remunit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-resize.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-scrollbars.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-subpixelfont.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-supports.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-userselect.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-vhunit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-vmaxunit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-vminunit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/css-vwunit.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/custom-protocol-handler.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dart.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dataview-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dom-classlist.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dom-createelement-attrs.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dom-dataset.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/dom-microdata.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-datalist.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-details.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-output.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-progress-meter.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-ruby.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-time.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/elem-track.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/emoji.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/es5-strictmode.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/event-deviceorientation-motion.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/exif-orientation.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/file-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/file-filesystem.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-fileinput.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-formattribute.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-inputnumber-l10n.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-placeholder.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-speechinput.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/forms-validation.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/fullscreen-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/gamepad.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/getusermedia.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/ie8compat.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/iframe-sandbox.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/iframe-seamless.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/iframe-srcdoc.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/img-apng.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/img-webp.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/json.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/lists-reversed.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/mathml.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/network-connection.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/network-eventsource.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/network-xhr2.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/notification.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/performance.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/pointerlock-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/quota-management-api.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/requestanimationframe.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/script-async.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/script-defer.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/style-scoped.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/svg-filters.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/unicode.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/url-data-uri.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/userdata.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/vibration.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/web-intents.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/webgl-extensions.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/websockets-binary.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/window-framed.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/workers-blobworkers.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/workers-dataworkers.js" />
/// <reference path="vendor/bower_components/modernizr/feature-detects/workers-sharedworkers.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.abide.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.alerts.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.clearing.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.cookie.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.dropdown.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.forms.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.interchange.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.joyride.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.magellan.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.orbit.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.placeholder.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.reveal.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.section.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.tooltips.js" />
/// <reference path="vendor/bower_components/bower-foundation/js/foundation/foundation.topbar.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.abide.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.accordion.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.alert.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.clearing.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.dropdown.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.equalizer.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.interchange.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.joyride.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.magellan.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.offcanvas.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.orbit.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.reveal.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.slider.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.tab.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.tooltip.js" />
/// <reference path="vendor/bower_components/foundation/js/foundation/foundation.topbar.js" />
/// <reference path="vendor/bower_components/foundation/js/vendor/fastclick.js" />
/// <reference path="vendor/bower_components/foundation/js/vendor/jquery.cookie.js" />
/// <reference path="vendor/bower_components/foundation/js/vendor/jquery.js" />
/// <reference path="vendor/bower_components/foundation/js/vendor/modernizr.js" />
/// <reference path="vendor/bower_components/foundation/js/vendor/placeholder.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/jsonp.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/load.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/parsejson.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/parsexml.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/script.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/xhr.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes/attr.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes/classes.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes/prop.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes/support.js" />
/// <reference path="vendor/bower_components/jquery/src/attributes/val.js" />
/// <reference path="vendor/bower_components/jquery/src/core/access.js" />
/// <reference path="vendor/bower_components/jquery/src/core/init.js" />
/// <reference path="vendor/bower_components/jquery/src/core/parsehtml.js" />
/// <reference path="vendor/bower_components/jquery/src/core/ready.js" />
/// <reference path="vendor/bower_components/jquery/src/css/addgethookif.js" />
/// <reference path="vendor/bower_components/jquery/src/css/curcss.js" />
/// <reference path="vendor/bower_components/jquery/src/css/defaultdisplay.js" />
/// <reference path="vendor/bower_components/jquery/src/css/hiddenvisibleselectors.js" />
/// <reference path="vendor/bower_components/jquery/src/css/support.js" />
/// <reference path="vendor/bower_components/jquery/src/css/swap.js" />
/// <reference path="vendor/bower_components/jquery/src/data/accepts.js" />
/// <reference path="vendor/bower_components/jquery/src/data/data.js" />
/// <reference path="vendor/bower_components/jquery/src/effects/animatedselector.js" />
/// <reference path="vendor/bower_components/jquery/src/effects/tween.js" />
/// <reference path="vendor/bower_components/jquery/src/event/alias.js" />
/// <reference path="vendor/bower_components/jquery/src/event/support.js" />
/// <reference path="vendor/bower_components/jquery/src/exports/amd.js" />
/// <reference path="vendor/bower_components/jquery/src/exports/global.js" />
/// <reference path="vendor/bower_components/jquery/src/manipulation/_evalurl.js" />
/// <reference path="vendor/bower_components/jquery/src/manipulation/support.js" />
/// <reference path="vendor/bower_components/jquery/src/queue/delay.js" />
/// <reference path="vendor/bower_components/jquery/src/traversing/findfilter.js" />
/// <reference path="vendor/bower_components/jquery/src/var/arr.js" />
/// <reference path="vendor/bower_components/jquery/src/var/class2type.js" />
/// <reference path="vendor/bower_components/jquery/src/var/concat.js" />
/// <reference path="vendor/bower_components/jquery/src/var/hasown.js" />
/// <reference path="vendor/bower_components/jquery/src/var/indexof.js" />
/// <reference path="vendor/bower_components/jquery/src/var/pnum.js" />
/// <reference path="vendor/bower_components/jquery/src/var/push.js" />
/// <reference path="vendor/bower_components/jquery/src/var/rnotwhite.js" />
/// <reference path="vendor/bower_components/jquery/src/var/slice.js" />
/// <reference path="vendor/bower_components/jquery/src/var/strundefined.js" />
/// <reference path="vendor/bower_components/jquery/src/var/support.js" />
/// <reference path="vendor/bower_components/jquery/src/var/tostring.js" />
/// <reference path="vendor/bower_components/jquery/src/var/trim.js" />
/// <reference path="vendor/bower_components/modernizr/test/caniuse_files/ga.js" />
/// <reference path="vendor/bower_components/modernizr/test/caniuse_files/jquery.min.js" />
/// <reference path="vendor/bower_components/modernizr/test/caniuse_files/modernizr-1.7.min.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/dumpdata.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/setup.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/unit-caniuse.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/unit.js" />
/// <reference path="vendor/bower_components/modernizr/test/qunit/qunit.js" />
/// <reference path="vendor/bower_components/modernizr/test/qunit/run-qunit.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/var/nonce.js" />
/// <reference path="vendor/bower_components/jquery/src/ajax/var/rquery.js" />
/// <reference path="vendor/bower_components/jquery/src/core/var/rsingletag.js" />
/// <reference path="vendor/bower_components/jquery/src/css/var/cssexpand.js" />
/// <reference path="vendor/bower_components/jquery/src/css/var/getstyles.js" />
/// <reference path="vendor/bower_components/jquery/src/css/var/ishidden.js" />
/// <reference path="vendor/bower_components/jquery/src/css/var/rmargin.js" />
/// <reference path="vendor/bower_components/jquery/src/css/var/rnumnonpx.js" />
/// <reference path="vendor/bower_components/jquery/src/data/var/data_priv.js" />
/// <reference path="vendor/bower_components/jquery/src/data/var/data_user.js" />
/// <reference path="vendor/bower_components/jquery/src/manipulation/var/rcheckabletype.js" />
/// <reference path="vendor/bower_components/jquery/src/sizzle/dist/sizzle.js" />
/// <reference path="vendor/bower_components/jquery/src/traversing/var/rneedscontext.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/lib/detect-global.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/lib/jquery-1.7b2.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/lib/jsonselect.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/lib/polyfills.js" />
/// <reference path="vendor/bower_components/modernizr/test/js/lib/uaparser.js" />
