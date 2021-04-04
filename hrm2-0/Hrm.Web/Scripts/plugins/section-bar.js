﻿"function" != typeof Object.create && (Object.create = function (t) { "use strict"; function i() { } return i.prototype = t, new i }), function (t, i, n, e) { "use strict"; var o = { Scroll: {}, Window: {}, init: function (n, e, o) { this.opt = t.extend({}, t.fn.pinBox.defaults, e), this.elem = n, this.Prepare(n), this.OnScroll(n), this.OnResize(n); var r = this; setTimeout(function () { for (var n = o.length - 1; n >= 0; n--) r.fixContainerHeight(o[n]); t(i).trigger("scroll") }, 1e3), t(n).on("pinBox.reload", function () { r.Reload(n) }) }, Prepare: function (i) { var n = t(i), e = {}, o = {}, r = {}, a = this.ExtractData(n), s = a.pinBoxOptions || this.opt || {}; if (s.Disabled === !1) { n.parent().is(".pinBox-wrapper") || (n.wrap("<div class='pinBox-wrapper'>"), n.parent().css({ position: "relative" })); var p = n.closest(s.Container), l = p.offset(); e.top = l.top, e.left = l.left, e.width = p.width(), e.height = p.height(); var d = n.offsetParent().offset(); o.top = d.top, o.left = d.left, n.parent().css("height", n.outerHeight()), r.width = n.outerWidth(), r.position = "fixed", r.left = parseFloat(o.left), r.top = s.Top, r["z-index"] = s.ZIndex, n.data("pinBox", JSON.stringify(r)).data("pinBoxOptions", JSON.stringify(s)).data("pinBoxParent", JSON.stringify(o)).data("pinBoxContainer", JSON.stringify(e)) } }, ExtractData: function (t) { var i = {}, n = t.data("pinBox") || {}, e = t.data("pinBoxOptions") || {}, o = t.data("pinBoxParent") || {}, r = t.data("pinBoxContainer") || {}; return "string" == typeof n && (i.pinBox = JSON.parse(n)), "string" == typeof e && (i.pinBoxOptions = JSON.parse(e)), "string" == typeof o && (i.pinBoxParent = JSON.parse(o)), "string" == typeof r && (i.pinBoxContainer = JSON.parse(r)), i }, OnScroll: function (n) { var e = this; t(i).scroll(function () { var o = t(n), r = t(this).scrollTop(), a = e.ExtractData(o), s = a.pinBox, p = a.pinBoxOptions, l = a.pinBoxParent, d = a.pinBoxContainer; if (e.Scroll.direction = r > e.Scroll.current ? "down" : "up", e.Scroll.current = r, e.Window.width = i.innerWidth || t(i).width(), r > d.top - parseInt(p.Top) && e.Window.width > parseInt(p.MinWidth)) { p.Disabled = !1; var c = o.closest(p.Container).height() - o.outerHeight(), h = (l.top || d.top) + c - parseInt(p.Top); r > h ? o.attr("style", "").css({ width: s.width, position: "absolute", top: c }) : o.css(s), e.CallEvents(o, !0, p.Disabled) } else e.Window.width <= parseInt(p.MinWidth) ? p.Disabled === !1 && (o.attr("style", "").unwrap(".pinBox-wrapper"), p.Disabled = !0) : o.attr("style", "").css({ width: s.width }), e.CallEvents(o, !1, p.Disabled); o.data("pinBoxOptions", JSON.stringify(p)) }) }, OnResize: function (n) { var e = this, o = t(n); t(i).resize(function () { var r = e.ExtractData(o), a = r.pinBoxOptions; if (e.Window.width = i.innerWidth || t(i).width(), a.Disabled = e.Window.width > parseInt(a.MinWidth) ? !1 : !0, a.Disabled === !1) { var s = o.parent().width(); o.attr("style", "").css({ width: s }) } else o.attr("style", ""); o.data("pinBoxOptions", JSON.stringify(a)), e.Prepare(n), e.CallEvents(o, !0, a.Disabled), t(i).trigger("scroll") }) }, CallEvents: function (t, i, n) { var e = this; i ? t.addClass("active") : t.removeClass("active"), "function" == typeof e.opt.Events && e.opt.Events.call(t, { current: e.Scroll.current, direction: e.Scroll.direction, width: e.Window.width, active: i, disabled: n }) }, fixContainerHeight: function (n) { var e = this, o = t(n), r = e.ExtractData(o), a = r.pinBoxOptions, s = r.pinBoxParent, p = r.pinBoxContainer, l = t(i).scrollTop(), d = o.closest(a.Container).height() - o.outerHeight(), c = (s.top || p.top) + d - parseInt(a.Top); l > c && o.attr("style", "").css({ width: r.pinBox.width, position: "absolute", top: d, transition: ".3s" }) }, Reload: function (i) { var n = t(i), e = n.parent().width(); n.attr("style", "").css({ width: e }), this.Prepare(i) } }; t.fn.pinBox = function (t) { var i = [], n = Object.create(o); return this.each(function () { i.push(this), n.init(this, t, i) }) }, t.fn.pinBox.defaults = { Container: ".container", Top: 0, ZIndex: 20, MinWidth: "767px", Events: !1, Disabled: !1 } }(jQuery, window, document);