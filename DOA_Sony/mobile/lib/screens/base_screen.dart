import 'package:flutter/material.dart';
import 'package:scoped_model/scoped_model.dart';

import '../service_locator.dart';

class BaseScreen<T extends Model> extends StatefulWidget {
  final ScopedModelDescendantBuilder<T> _builder;

  final Function(T) onModelReady;

  BaseScreen({
    ScopedModelDescendantBuilder<T> builder,
    this.onModelReady,
  }) : _builder = builder;

  @override
  _BaseScreenState<T> createState() => _BaseScreenState<T>();
}

class _BaseScreenState<T extends Model> extends State<BaseScreen<T>> {
  T _model = locator<T>();
  bool connection = false;
  String connectionMessage = "";

  @override
  void initState() {
    if (widget.onModelReady != null) {
      widget.onModelReady(_model);
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return ScopedModel<T>(
      model: _model,
      child: ScopedModelDescendant<T>(
        builder: widget._builder,
      ),
    );
  }
}
